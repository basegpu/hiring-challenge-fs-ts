namespace TsApi.Models
{
    public class Asset
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public float Latitude { get; set; } = 0;
        public float Longitude { get; set; } = 0;
    }
} 