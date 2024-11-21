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
    public class MemoryAssetRepository : IAssetRepository
    {
        public MemoryAssetRepository()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "data", "assets.json");
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var assets = JsonConvert.DeserializeObject<List<Asset>>(json);
                if (assets != null)
                {
                    foreach (var asset in assets)
                    {
                        _assets[asset.Id] = asset;
                    }
                }
            }
        }
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