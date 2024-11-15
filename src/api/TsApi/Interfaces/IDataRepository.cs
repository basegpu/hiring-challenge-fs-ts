using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsApi.Models;

namespace TsApi.Interfaces
{
    public interface IDataRepository
    {
        Task<IEnumerable<TimeSeriesData>> GetDataAsync(int signalId, DateTime? from = null, DateTime? to = null);
    }
} 