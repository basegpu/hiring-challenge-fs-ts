using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsApi.Data;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class DbSignalRepository : ISignalRepository
    {
        private readonly TsDbContext _context;

        public DbSignalRepository(TsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Signal>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Signal>>(_context.Signals);
        }

        public Task<IEnumerable<Signal>> GetByAssetIdAsync(int assetId)
        {
            return Task.FromResult<IEnumerable<Signal>>(
                _context.Signals.Where(s => s.AssetId == assetId)
            );
        }

        public async Task<Signal?> GetByIdAsync(int id)
        {
            return await _context.Signals.FindAsync(id);
        }
    }
} 