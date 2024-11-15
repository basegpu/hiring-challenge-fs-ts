using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class MemorySignalRepository : ISignalRepository
    {
        private readonly Dictionary<int, Signal> _signals = new();

        public Task<IEnumerable<Signal>> GetAllAsync()
        {
            return Task.FromResult(_signals.Values.AsEnumerable());
        }

        public Task<IEnumerable<Signal>> GetByAssetIdAsync(int assetId)
        {
            return Task.FromResult(
                _signals.Values.Where(s => s.AssetId == assetId)
            );
        }

        public Task<Signal?> GetByIdAsync(int id)
        {
            _signals.TryGetValue(id, out var signal);
            return Task.FromResult(signal);
        }
    }
} 