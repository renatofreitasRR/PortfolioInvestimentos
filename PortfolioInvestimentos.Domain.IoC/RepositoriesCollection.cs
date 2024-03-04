using Microsoft.Extensions.DependencyInjection;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Infra.Repositories;

namespace PortfolioInvestimentos.Domain.IoC
{
    public static class RepositoriesCollection
    {
        public static IServiceCollection AddRepositoriesCollection(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
