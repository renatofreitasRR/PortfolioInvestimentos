using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators.Users
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("O campo Email está inválido");

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("O campo Senha está inválido");

        }
    }
}
