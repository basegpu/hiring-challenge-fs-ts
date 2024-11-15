using Newtonsoft.Json;
using System;

namespace TsApi.Models
{
    public class Signal
    {
        [JsonProperty("SignalID", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("SignalGId", Required = Required.Always)]
        public Guid Guid { get; set; }

        [JsonProperty("AssetID", Required = Required.Always)]
        public int AssetId { get; set; }

        [JsonProperty("SignalName", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("Unit", Required = Required.Always)]
        public string Unit { get; set; }
    }
} 