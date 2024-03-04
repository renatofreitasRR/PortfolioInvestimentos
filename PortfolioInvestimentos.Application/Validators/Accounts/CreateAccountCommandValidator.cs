using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Accounts;

namespace PortfolioInvestimentos.Application.Validators.Accounts
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("O campo Id do usuário está inválido");

            RuleFor(x => x.Value)
           .NotNull()
           .WithMessage("O campo Valor deve ter um valor válido");

        }
    }
}
