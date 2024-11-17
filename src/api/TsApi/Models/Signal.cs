using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TsApi.Models
{
    public class Signal
    {
        [Key]
        [Column("id", TypeName = "integer")]
        [JsonProperty("SignalID", Required = Required.Always)]
        public int Id { get; set; }

        [Column("guid", TypeName = "uuid")]
        [JsonProperty("SignalGId", Required = Required.Always)]
        public Guid Guid { get; set; }

        [Column("asset_id", TypeName = "integer")]
        [JsonProperty("AssetID", Required = Required.Always)]
        public int AssetId { get; set; }

        [Column("name", TypeName = "text")]
        [JsonProperty("SignalName", Required = Required.Always)]
        public string Name { get; set; }

        [Column("unit", TypeName = "text")]
        [JsonProperty("Unit", Required = Required.Always)]
        public string Unit { get; set; }
    }
} 