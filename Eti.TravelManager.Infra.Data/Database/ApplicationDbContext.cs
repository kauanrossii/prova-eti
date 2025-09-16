using Eti.TravelManager.Domain.Destinos.Entities;
using Eti.TravelManager.Domain.Viagens.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Infra.Data.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Viagem> Viagens { get; set; }
    public DbSet<Destino> Destinos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Viagem>()
            .HasMany(e => e.Destinos);
    }
}