using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Models;

namespace RendaVariavelApi.Data.Configuration
{
    public class DividendoConfiguration : IEntityTypeConfiguration<Dividendo>
    {
        public void Configure(EntityTypeBuilder<Dividendo> builder)
        {
            builder.ToTable("DIVIDENDOS");

            builder.HasKey(d => d.IdDividendo);
            builder.Property(d => d.IdDividendo)
                .HasColumnName("ID_DIVIDENDO")
                .UseIdentityColumn(1, 1);

            builder.Property(fi => fi.Ticker)
               .HasColumnName("TICKER")
               .IsRequired()
               .HasMaxLength(15);

            builder.Property(fi => fi.TipoDividendo)
                .HasColumnType("int")
                .HasColumnName("TIPO_DIVIDENDO");

            builder.Property(d => d.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.DataCom)
                .HasColumnName("DATA_COM")
                .HasColumnType("date");

            builder.Property(d => d.DataPagamento)
                .HasColumnName("DATA_PAGAMENTO")
                .HasColumnType("date");

        }
    }
}