using RendaVariavelApi.Enums;
using RendaVariavelApi.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RendaVariavelApi.Models
{
    public class Dividendo
    {
        [JsonIgnore]
        //[Key]
        public int IdDividendo { get; set; }

        [Required]
        //[ForeignKey("FundosImobiliarios")]
        [StringLength(15)]
        public string Ticker;
        public DividendoType TipoDividendo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateCheck]
        public DateTime DataCom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateCheck]
        public DateTime DataPagamento { get; set; }

        //[Required]
        //[Column(TypeName = "decimal(18,2)")]
        public double Valor { get; set; }

        //[JsonIgnore]
        //public FundosImobiliarios? FundoImobiliario { get; set; }
        public Dividendo(){}
        public Dividendo(string ticker,string tipoDividendo, string dataCom, string dataPagamento, double valor)
        {
            Ticker = ticker;
            TipoDividendo = StringToDividendoType(tipoDividendo);
            DataCom = stringDateToDateTime(dataCom);
            DataPagamento = stringDateToDateTime(dataPagamento);
            Valor = valor;
        }
        private protected static DateTime stringDateToDateTime(string stringDate)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(stringDate, new string[] { "dd/MM/yyyy", "yyyy-MM-dd" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return DateTime.ParseExact(parsedDate.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                throw new ArgumentException("Data inválida. Certifique-se de fornecer uma data no formato 'dd/MM/yyyy' ou 'yyyy-MM-dd'.");
            }
        }
        private protected DividendoType StringToDividendoType(string value) {
            if (Enum.TryParse(value, out DividendoType tipo)) return tipo;
            throw new ArgumentException($"Tipo de dividendo inválido: {value}");
        }
  
    }
}
