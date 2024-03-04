using FluentValidation;
using PortfolioInvestimentos.Domain.Commands.Products;

namespace PortfolioInvestimentos.Application.Validators.Products
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty()
           .NotNull()
           .WithMessage("O campo Id está inválido");

            RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255)
            .WithMessage("O campo Nome está inválido, digite entre 5 e 255 caracteres");

            RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("O campo Tipo deve ter um valor válido");

            RuleFor(x => x.Value)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("O campo Valor deve ter um valor válido e ser maior do que 0");

            RuleFor(x => x.DueDate)
           .NotNull()
           .GreaterThan(DateTime.UtcNow)
           .WithMessage("O campo Data de Vencimento deve ter um valor válido e com vencimento maior do que a data atual");
        }
    }
}
