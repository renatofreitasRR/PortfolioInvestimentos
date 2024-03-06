using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PortfolioInvestimentos.Application.Authentication;
using PortfolioInvestimentos.Application.Caching;
using PortfolioInvestimentos.Application.Caching.Contracts;
using PortfolioInvestimentos.Application.Jobs.Email;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Services;

namespace PortfolioInvestimentos.Domain.IoC
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddServicesCollection(this IServiceCollection services)
        {
            services.AddSingleton<ICachingService, CachingService>();
            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserPasswordService, UserPasswordService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
