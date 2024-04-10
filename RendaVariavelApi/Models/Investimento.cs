using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RendaVariavelApi.Enums;
using RendaVariavelApi.Validators;

namespace RendaVariavelApi.Models
{
    public abstract class Investimento
    {
        [Required]
        [FundoImobiliarioTickerCheck]
        [StringLength(15)]
        
        public string Ticker { get; set; }
        
        //[Required]
        [InvestimentoTypeCheck]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InvestimentoType TipoInvestimento { get; set; }
        
        [StringLength(14)]
        public string? Cnpj { get; set; }
        
        [StringLength(255)]
        public string? RazaoSocial { get; set; }
        
        public Investimento() { }   
        public Investimento(string ticker, string tipoInvestimento)
        {
            //if (string.IsNullOrEmpty(ticker)) { throw new ArgumentException("Ticker vazio ou em branco. Certifique-se de preenchê-lo"); }
            Ticker = ticker;
            TipoInvestimento = StringToInvestimentoType(tipoInvestimento); //StringToInvestimentoType(tipoInvestimento);
        }
        private protected InvestimentoType StringToInvestimentoType(string value)
        {
            if (Enum.TryParse(value, out InvestimentoType tipo)) return tipo;
            throw new ArgumentException($"Tipo de investimento inválido: {value}");
        }
    }
}
