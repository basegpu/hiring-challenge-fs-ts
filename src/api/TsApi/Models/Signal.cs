using System;

namespace TsApi.Models
{
    public class Signal
    {
        public int Id { get; set; } = 0;
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int AssetId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
} 