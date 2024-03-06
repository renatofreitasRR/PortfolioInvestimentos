using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Infra.Context;

namespace PortfolioInvestimentos.Domain.Api.Configurations
{
    public static class DatabaseManagementConfiguration
    {
        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<ApplicationDbContext>()
                    .Database
                    .Migrate();
            }
        }
    }
}
