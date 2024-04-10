using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RendaVariavelApi.Models;

namespace RendaVariavelApi.Data.Configuration
{
    public class FundoImobiliarioConfiguration : IEntityTypeConfiguration<FundoImobiliario>
    {
        public void Configure(EntityTypeBuilder<FundoImobiliario> builder)
        {
            builder.ToTable("FUNDOS_IMOBILIARIOS");

            builder.HasKey(fi => fi.IdFundoImobiliario);
            builder.Property(f => f.IdFundoImobiliario)
                .HasColumnName("ID_FUNDO_IMOBILIARIO")
                .UseIdentityColumn(1, 1);

            builder.Property(fi => fi.Ticker)
               .IsRequired()
               .HasMaxLength(15)
               .HasColumnName("TICKER");

            builder.HasIndex(f => f.Ticker).IsUnique();

            builder.Property(fi => fi.TipoInvestimento)
               .HasColumnType("int")
               .HasColumnName("TIPO_INVESTIMENTO");

            builder.Property(fi => fi.Cnpj)
               .HasMaxLength(14)
               .HasColumnName("CNPJ");

            builder.Property(fi => fi.RazaoSocial)
                .HasMaxLength(255)
                .HasColumnName("RAZAO_SOCIAL");

            builder.Property(fi => fi.Segmento)
                .HasMaxLength(255)
                .HasColumnName("SEGMENTO");

            builder.Property(fi => fi.PublicoAlvo)
                .HasMaxLength(255)
                .HasColumnName("PUBLICO_ALVO");

            builder.Property(fi => fi.Mandato)
                .HasMaxLength(255)
                .HasColumnName("MANDATO");

            builder.Property(fi => fi.TipoDeFundo)
                .HasMaxLength(255)
                .HasColumnName("TIPO_FUNDO");

            builder.Property(fi => fi.PrazoDeDuracao)
                .HasMaxLength(255)
                .HasColumnName("PRAZO_DURACAO");

            builder.Property(fi => fi.TipoDeGestao)
                .HasMaxLength(255)
                .HasColumnName("TIPO_GESTAO");

            builder.Property(fi => fi.TaxaDeAdministracao)
                .HasColumnName("TAXA_ADMINISTRACAO");

            builder.Property(fi => fi.Vacancia)
                .HasColumnName("VACANCIA");

            builder.Property(fi => fi.NumeroDeCotistas)
                .HasColumnName("NUMERO_COTISTAS");

            builder.Property(fi => fi.CotasEmitidas)
                .HasColumnName("COTAS_EMITIDAS");

            builder.Property(fi => fi.ValorPatrimonialPorCota)
                .HasColumnName("VALOR_PATRIMONIAL_POR_COTA");

            builder.Property(fi => fi.ValorPatrimonial)
                .HasColumnName("VALOR_PATRIMONIAL");

            builder.Property(fi => fi.UltimoRendimento)
                .HasColumnName("ULTIMO_RENDIMENTO");

        }
    }
}
