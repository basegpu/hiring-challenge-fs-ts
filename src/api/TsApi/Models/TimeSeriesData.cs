using System;

namespace TsApi.Models
{
    public class TimeSeriesData
    {
        public int SignalId { get; set; } = 0;
        public DateTime Timestamp { get; set; } = DateTime.MinValue;
        public double Value { get; set; } = 0;
    }
} 