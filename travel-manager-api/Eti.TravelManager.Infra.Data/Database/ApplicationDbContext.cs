using Eti.TravelManager.Domain.Destinos.Entities;
using Eti.TravelManager.Domain.Viagens.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTime>(
            v => v.UtcDateTime,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder.Entity<Viagem>()
            .HasMany(e => e.Destinos)
            .WithMany(e => e.Viagens);
        
        modelBuilder.Entity<Viagem>()
            .Property(e => e.DataSaida)
            .HasConversion(dateTimeOffsetConverter);
        
        modelBuilder.Entity<Viagem>()
            .Property(e => e.DataChegada)
            .HasConversion(dateTimeOffsetConverter);
    }
}