using Microsoft.EntityFrameworkCore;

namespace ModularMonolith.Database.DatabaseContexts;
public sealed class ModularMonolithDbContext(DbContextOptions<ModularMonolithDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModularMonolithDbContext).Assembly);
    }
}
