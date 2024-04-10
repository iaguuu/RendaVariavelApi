using RendaVariavelApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RendaVariavelApi.Models
{
    public class FundoImobiliario : Investimento
    {
        [JsonIgnore]
        public int IdFundoImobiliario { get; set; }
        
        [StringLength(255)]
        public string? Segmento { get; set; }

        [StringLength(255)]
        public string? PublicoAlvo { get; set; }

        [StringLength(255)]
        public string? Mandato { get; set; }

        [StringLength(255)]
        public string? TipoDeFundo { get; set; }

        [StringLength(255)]
        public string? PrazoDeDuracao { get; set; }

        [StringLength(255)]
        public string? TipoDeGestao { get; set; }

        public double? TaxaDeAdministracao { get; set; }
        public double? Vacancia { get; set; }
        public int? NumeroDeCotistas { get; set; }
        public int? CotasEmitidas { get; set; }
        public double? ValorPatrimonialPorCota { get; set; }
        
        public double? ValorPatrimonial {  get; set; }  
        public double? UltimoRendimento { get; set; }
        public FundoImobiliario() { }
        public FundoImobiliario(string ticker, string tipoInvestimento) : base(ticker, tipoInvestimento)
        {
            //if (!ticker.EndsWith("11")) { throw new ArgumentException("O ticker do fundo imobiliário deve terminar com '11';"); }
            Ticker = ticker;
            TipoInvestimento = StringToInvestimentoType(tipoInvestimento);
        }
    }
}
