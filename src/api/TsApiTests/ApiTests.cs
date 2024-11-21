using Newtonsoft.Json;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using TsApi.Models;

namespace TsApiTest;

public class ApiTests
{
    private HttpClient _client = null!;
    private TestWebApplicationFactory _factory = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _factory = new TestWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetAssets_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/assets");
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        var assets = JsonConvert.DeserializeObject<IEnumerable<Asset>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        assets.Should().NotBeNull();
    }

    [Test]
    public async Task GetAsset_WithValidId_ReturnsAsset()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _client.GetAsync($"/api/assets/{validId}");
        var content = await response.Content.ReadAsStringAsync();
        var asset = JsonConvert.DeserializeObject<Asset>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        asset.Should().NotBeNull();
        asset!.Id.Should().Be(validId);
    }

    [Test]
    public async Task GetAsset_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var response = await _client.GetAsync($"/api/assets/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetSignals_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/signals");
        var content = await response.Content.ReadAsStringAsync();
        var signals = JsonConvert.DeserializeObject<IEnumerable<Signal>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        signals.Should().NotBeNull();
    }

    [Test]
    public async Task GetSignals_WithAssetId_ReturnsSignalsForAsset()
    {
        // Arrange
        var assetId = 1;

        // Act
        var response = await _client.GetAsync($"/api/signals?assetId={assetId}");
        var content = await response.Content.ReadAsStringAsync();
        var signals = JsonConvert.DeserializeObject<IEnumerable<Signal>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        signals.Should().NotBeNull();
        signals!.Should().AllSatisfy(s => s.AssetId.Should().Be(assetId));
    }

    [Test]
    public async Task GetSignalData_WithValidParameters_ReturnsData()
    {
        // Arrange
        var signalId = 1;
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow;

        // Act
        var response = await _client.GetAsync($"/api/data?signalId={signalId}&from={from:O}&to={to:O}");
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<TimeSeriesData>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data!.Should().AllSatisfy(d => 
        {
            d.SignalId.Should().Be(signalId);
            d.Timestamp.Should().BeOnOrAfter(from);
            d.Timestamp.Should().BeOnOrBefore(to);
        });
    }

    [Test]
    public async Task GetHealth_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
