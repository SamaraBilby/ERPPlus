using ERPPlus.Application.DTOs;
using FluentValidation;


namespace ERPPlus.Application.Validators
{
    public class CreateCustomerDTOValidator : AbstractValidator<CreateCustomerDTO>
    {
       public CreateCustomerDTOValidator() { 
        
                RuleFor(x => x.Name).NotEmpty()
                    .WithMessage("O nome é obrigatório.");

                RuleFor(x => x.Email).NotEmpty()
                    .WithMessage("O e-mail é obrigatório.")
                    .EmailAddress()
                    .WithMessage("Formato inválido");

                RuleFor(x => x.PasswordHash).NotEmpty()
                    .WithMessage("O senha é obrigatório.")
                    .MinimumLength(6)
                    .WithMessage("Mínimo 6 caracteres.");

        }
    }
}
