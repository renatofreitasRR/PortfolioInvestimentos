using Microsoft.Extensions.DependencyInjection;
using PortfolioInvestimentos.Domain.Handlers;

namespace PortfolioInvestimentos.Domain.IoC
{
    public static class HandlersCollection
    {
        public static IServiceCollection AddHandlersCollection(this IServiceCollection services)
        {
            services.AddTransient<AccountHandler>();
            services.AddTransient<TransactionHandler>();
            services.AddTransient<UserHandler>();
            services.AddTransient<ProductHandler>();
            
            return services;
        }
    }
}
