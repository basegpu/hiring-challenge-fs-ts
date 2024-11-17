using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsApi.Data;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class DbDataRepository : IDataRepository
    {
        private readonly TsDbContext _context;

        public DbDataRepository(TsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<TimeSeriesData>> GetDataAsync(int signalId, DateTime? from = null, DateTime? to = null)
        {
            var query = _context.TimeSeriesData.Where(d => d.SignalId == signalId);

            if (from.HasValue)
                query = query.Where(d => d.Timestamp >= from.Value);
            
            if (to.HasValue)
                query = query.Where(d => d.Timestamp <= to.Value);

            return Task.FromResult<IEnumerable<TimeSeriesData>>(query);
        }
    }
} 