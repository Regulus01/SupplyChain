using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps;

namespace SupplyChain.Infra.Data.Context;

public class MercadoriaDbContext : DbContext
{
    public DbSet<Mercadoria> Mercadorias { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Saida> Saidas { get; set; }
    public DbSet<TipoDeMercadoria> TipoDeMercadorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        Console.WriteLine("Aplicando migration na conexão -> " + connectionString);
        
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EntradaMap());
        modelBuilder.ApplyConfiguration(new SaidaMap());
        modelBuilder.ApplyConfiguration(new MercadoriaMap());
        modelBuilder.ApplyConfiguration(new TipoDeMercadoriaMap());
    }
}