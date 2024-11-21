using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class MemorySignalRepository : ISignalRepository
    {
        public MemorySignalRepository()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "data", "signals.json");
            var json = File.ReadAllText(jsonPath);
            var signals = JsonConvert.DeserializeObject<List<Signal>>(json);
            if (signals != null)
            {
                foreach (var signal in signals)
                {
                    _signals[signal.Id] = signal;
                }
            }
        }

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
            if (_signals.TryGetValue(id, out var signal))
            {
                return Task.FromResult<Signal?>(signal);
            }
            else
            {
                return Task.FromResult<Signal?>(null);
            }
        }
    }
} 