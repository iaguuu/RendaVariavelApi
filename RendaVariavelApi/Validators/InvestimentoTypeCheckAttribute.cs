using System.ComponentModel.DataAnnotations;

namespace RendaVariavelApi.Validators
{
    public class InvestimentoTypeCheckAttribute : ValidationAttribute //RequiredEnumAttribute : RequiredAttribute 
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"Tipo de investimento não definido");

            Type type = value.GetType();

            if (!type.IsEnum) return new ValidationResult($"Valor inválido");
            if (!Enum.IsDefined(type, value)) return new ValidationResult($"Tipo de investimento inválido: {value}");

            return ValidationResult.Success;
        }

    }
}
