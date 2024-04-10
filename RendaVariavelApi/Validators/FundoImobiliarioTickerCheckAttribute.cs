using System.ComponentModel.DataAnnotations;

namespace RendaVariavelApi.Validators
{
    public class FundoImobiliarioTickerCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"Ticker não preenchido");
            string? ticker = (string?)value;

            if (string.IsNullOrEmpty(ticker) || string.IsNullOrWhiteSpace(ticker)) { return new ValidationResult("Ticker vazio ou em branco. Certifique-se de preenchê-lo"); }                     
            if (!ticker.EndsWith("11")) { return new ValidationResult("O ticker do fundo imobiliário deve terminar com '11'"); }
            if (ticker.Length < 6) { return new ValidationResult("O ticker deve ter ao menos 6 caracteres"); }

            return ValidationResult.Success;
        }

    }
}
