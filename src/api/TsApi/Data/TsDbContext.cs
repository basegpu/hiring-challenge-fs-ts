using Microsoft.EntityFrameworkCore;
using TsApi.Models;

namespace TsApi.Data;


public class TsDbContext : DbContext
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Signal> Signals { get; set; }
    public DbSet<TimeSeriesData> TimeSeriesData { get; set; }

    public TsDbContext(DbContextOptions<TsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>()
            .ToTable("assets");

        modelBuilder.Entity<Signal>()
            .ToTable("signals");
            
        modelBuilder.Entity<TimeSeriesData>()
            .ToTable("measurements")
            .HasNoKey();
    }
}
