using AutoMapper;
using PortfolioInvestimentos.Application.Mappings;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models.Users;

namespace PortfolioInvestimentos.Domain.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappingProfile));
        }
    }
}
