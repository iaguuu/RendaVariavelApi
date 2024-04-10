using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Models;

namespace RendaVariavelApi.Data.Configuration
{
    public class CotacaoConfigurarion : IEntityTypeConfiguration<Cotacao>
    {
        public void Configure(EntityTypeBuilder<Cotacao> builder)
        {
            builder.ToTable("COTACOES");

            builder.HasKey(d => d.IdCotacao);
            builder.Property(d => d.IdCotacao)
                .HasColumnName("ID_DIVIDENDO")
                .UseIdentityColumn(1, 1);

            builder.Property(fi => fi.Ticker)
               .HasColumnName("TICKER")
               .IsRequired()
               .HasMaxLength(15);

            //builder.HasOne(y => y.FundoImobiliario)
            //   .WithMany(x => x.Cotacoes)
            //   .HasForeignKey(x => x.Ticker);

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("VALOR");

            builder.Property(c => c.Data)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("DATA_COTACAO");

        }
    }
}
