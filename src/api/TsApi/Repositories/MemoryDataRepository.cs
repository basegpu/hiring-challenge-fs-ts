using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.IO;
using System;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class MemoryDataRepository : IDataRepository
    {
        public MemoryDataRepository()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "data", "measurements.csv");
            if (File.Exists(jsonPath))
            {
                _data = File.ReadAllLines(jsonPath)
                    .Skip(1)
                .Select(line => line.Split('|'))
                .Select(columns =>
                {
                    try
                    {
                        return new TimeSeriesData
                        {
                            Timestamp = DateTime.ParseExact(columns[0], "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                            SignalId = int.Parse(columns[1]),
                            Value = double.Parse(columns[2].Replace(',', '.'), CultureInfo.InvariantCulture)
                        };
                    }
                    catch (FormatException ex)
                    {
                        // Log the error and skip the malformed line
                        Console.WriteLine($"Error parsing line: {string.Join('|', columns)} - {ex.Message}");
                        return null;
                    }
                    })
                    .Where(data => data != null)
                    .Cast<TimeSeriesData>();
            }
        }

        private readonly IEnumerable<TimeSeriesData> _data = new List<TimeSeriesData>();

        public Task<IEnumerable<TimeSeriesData>> GetDataAsync(int signalId, DateTime? from = null, DateTime? to = null)
        {
            var signalData = _data.Where(d => d.SignalId == signalId);

            var query = signalData.AsEnumerable();
            if (from.HasValue)
            {
                query = query.Where(d => d.Timestamp >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(d => d.Timestamp <= to.Value);
            }
            return Task.FromResult(query);
        }
    }
} 