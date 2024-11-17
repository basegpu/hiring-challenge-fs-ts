using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TsApi.Models
{
    public class Asset
    {
        [Key]
        [Column("id", TypeName = "integer")]
        [JsonProperty("AssetID", Required = Required.Always)]
        public int Id { get; set; }

        [Column("description", TypeName = "text")]
        [JsonProperty("descri", Required = Required.Always)]
        public string Description { get; set; }

        [Column("latitude", TypeName = "float")]
        [JsonProperty("Latitude", Required = Required.Always)]
        public float Latitude { get; set; }

        [Column("longitude", TypeName = "float")]
        [JsonProperty("Longitude", Required = Required.Always)] 
        public float Longitude { get; set; }
    }
} 