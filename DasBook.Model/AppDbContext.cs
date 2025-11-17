using Microsoft.EntityFrameworkCore;

namespace DasBook.Model;

public class AppDbContext : DbContext
{
    public AppDbContext() {}

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Universe> Universes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }


}