using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class MemoryAssetRepository : IAssetRepository
    {
        private readonly Dictionary<int, Asset> _assets = new();

        public Task<IEnumerable<Asset>> GetAllAsync()
        {
            return Task.FromResult(_assets.Values.AsEnumerable());
        }

        public Task<Asset?> GetByIdAsync(int id)
        {
            _assets.TryGetValue(id, out var asset);
            return Task.FromResult(asset);
        }
    }
} 