using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class MemoryDataRepository : IDataRepository
    {
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