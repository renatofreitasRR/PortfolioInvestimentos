using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PortfolioInvestimentos.Application.Validators.Accounts;
using PortfolioInvestimentos.Application.Validators.Products;
using PortfolioInvestimentos.Application.Validators.Transactions;
using PortfolioInvestimentos.Application.Validators.Users;

namespace PortfolioInvestimentos.Domain.IoC
{
    public static class ValidatorsCollection
    {
        public static IServiceCollection AddValidatorsCollection(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();
            
            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateTransactionCommandValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();

            return services;
        }
    }
}
