using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators.Accounts
{
    public class DepositAccountCommandValidator : AbstractValidator<DepositAccountCommand>
    {
        public DepositAccountCommandValidator()
        {
            RuleFor(x => x.Id)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("O campo Id está inválido");

            RuleFor(x => x.Value)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("O campo Valor deve ter um valor válido");
        }
    }
}
