using RendaVariavelApi.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RendaVariavelApi.Models
{
    public class Cotacao
    {
        [JsonIgnore]
        //[Key]
        public int IdCotacao { get; set; }

        [Required]
        //[ForeignKey("FundosImobiliarios")]
        [StringLength(15)]
        public string Ticker;

        [Required]
        //[Column(TypeName = "decimal(18,2)")]
        public double Valor { get; set; }
        
        [DateCheck]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        //[JsonIgnore]
        //public FundosImobiliarios? FundoImobiliario { get; set; }
        public Cotacao() { }
        public Cotacao(string ticker,double valor, string data)
        {
            Ticker = ticker;
            Valor = valor;
            Data = stringDateToDateTime(data);
        }
        private protected static DateTime stringDateToDateTime(string stringDate)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(stringDate, new string[] { "dd/MM/yyyy", "yyyy-MM-dd" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return DateTime.ParseExact(parsedDate.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            }

            throw new ArgumentException("Data inválida. Certifique-se de fornecer uma data no formato 'dd/MM/yyyy' ou 'yyyy-MM-dd'.");
        }

    }
}
