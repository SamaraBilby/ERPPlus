using ERPPlus.Application.DTOs;
using FluentValidation;

namespace ERPPlus.Application.Validators
{
    public class UpdateCustomerDTOValidator : AbstractValidator<UpdateCustomerDTO>
    {
        public UpdateCustomerDTOValidator() {

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("Formato inválido");

        }
    }
}
