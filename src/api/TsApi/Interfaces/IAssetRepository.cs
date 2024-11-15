using System.Collections.Generic;
using System.Threading.Tasks;
using TsApi.Models;

namespace TsApi.Interfaces
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(int id);
    }
} 