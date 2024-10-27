using ENSEKApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ENSEKApi.Infrastructure.SqlServer;

public class EnsekDbContext : DbContext
{
    public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options) { }

    public DbSet<MeterReading> MeterReadings { get; set; }
}
