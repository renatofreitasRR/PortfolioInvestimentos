using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators.Transactions
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O campo AccountId está inválido");

            RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O campo ProductId está inválido");

            RuleFor(x => x.OperationType)
            .IsInEnum()
            .WithMessage("O campo Tipo da Operação deve ter um valor válido");

            RuleFor(x => x.Value)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("O campo Valor deve ter um valor válido e ser maior do que 0");
        }
    }


}
