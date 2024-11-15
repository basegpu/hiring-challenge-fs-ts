using Newtonsoft.Json;

namespace TsApi.Models
{
    public class Asset
    {
        [JsonProperty("AssetID", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("descri", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("Latitude", Required = Required.Always)]
        public float Latitude { get; set; }

        [JsonProperty("Longitude", Required = Required.Always)] 
        public float Longitude { get; set; }
    }
} 