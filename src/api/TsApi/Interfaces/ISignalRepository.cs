using System.Collections.Generic;
using System.Threading.Tasks;
using TsApi.Models;

namespace TsApi.Interfaces
{
    public interface ISignalRepository
    {
        Task<IEnumerable<Signal>> GetAllAsync();
        Task<IEnumerable<Signal>> GetByAssetIdAsync(int assetId);
        Task<Signal?> GetByIdAsync(int id);
    }
} 