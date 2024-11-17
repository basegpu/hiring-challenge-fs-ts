using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsApi.Data;
using TsApi.Interfaces;
using TsApi.Models;

namespace TsApi.Repositories
{
    public class DbAssetRepository : IAssetRepository
    {
        private readonly TsDbContext _context;

        public DbAssetRepository(TsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Asset>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Asset>>(_context.Assets);
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _context.Assets.FindAsync(id);
        }
    }
} 