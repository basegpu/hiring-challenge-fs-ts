using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TsApi.Models
{
    public class TimeSeriesData
    {
        [Column("signal_id", TypeName = "integer")]
        public int SignalId { get; set; } = 0;

        [Column("timestamp", TypeName = "timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.MinValue;

        [Column("value", TypeName = "float")]
        public double Value { get; set; } = 0;
    }
} 