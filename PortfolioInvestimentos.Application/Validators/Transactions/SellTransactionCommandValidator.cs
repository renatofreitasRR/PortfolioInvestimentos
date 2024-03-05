using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators.Transactions
{
    public class SellTransactionCommandValidator : AbstractValidator<SellTransactionCommand>
    {
        public SellTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId)
             .NotEmpty()
             .NotNull()
             .WithMessage("O campo AccountId está inválido");

            RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O campo ProductId está inválido");

            RuleFor(x => x.Quantity)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("O campo Quantidade deve ter um valor válido e ser maior do que 0");
        }
    }


}
