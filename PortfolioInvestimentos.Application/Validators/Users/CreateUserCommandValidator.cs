using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Users;

namespace PortfolioInvestimentos.Application.Validators.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {

            RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255)
            .WithMessage("O campo Nome está inválido, digite entre 5 e 255 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("O campo Email está inválido");

            RuleFor(x => x.Profile)
            .IsInEnum()
            .WithMessage("O campo Perfil deve ter um valor válido");

            RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("O campo Role deve ter um valor válido");

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .Equal(x => x.ConfirmPassword)
            .WithMessage("O campo Senha está inválido ou não coincide com a confirmação de senha");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .Equal(x => x.Password)
            .WithMessage("O campo Confirmação de Senha está inválido ou não coincide com a senha");
        }
    }
}
