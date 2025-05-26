using ERPPlus.Application.DTOs;
using FluentValidation;

namespace ERPPlus.Application.Validators
{
    public class UpdateProductDTOValidatorcs : AbstractValidator<ProductDTO>
    {
        public UpdateProductDTOValidatorcs() 
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome do produto pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Price).GreaterThan(0)
                .WithMessage("O preço deve ser maio que zero.");
        }
    }
}
