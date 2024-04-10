using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Data.Configuration;
using RendaVariavelApi.Models;
using System.Reflection;

namespace RendaVariavelApi.Data
{
    public class RendaVariavelDbContext : DbContext
    {
        public RendaVariavelDbContext(DbContextOptions<RendaVariavelDbContext> options) : base(options)
        {
        }
        public DbSet<FundoImobiliario> FUNDOS_IMOBILIARIOS { get; set; }
        public DbSet<Dividendo> DIVIDENDOS { get; set; }
        public DbSet<Cotacao> COTACOES { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FundoImobiliarioConfiguration());
            modelBuilder.ApplyConfiguration(new DividendoConfiguration());
            modelBuilder.ApplyConfiguration(new CotacaoConfigurarion());
            base.OnModelCreating(modelBuilder);
        }

    }
}
