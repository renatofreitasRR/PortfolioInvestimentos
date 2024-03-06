using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Infra.Context;

namespace PortfolioInvestimentos.Domain.Api.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            var server = configuration["DbServer"] ?? "localhost";
            var port = configuration["DbPort"] ?? "1433"; // Default SQL Server port
            var user = configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
            var password = configuration["Password"] ?? "numsey#2021";
            var database = configuration["Database"] ?? "Portfolio";

            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Persist Security Info=False;Encrypt=False";

            //var connectionString = configuration.GetConnectionString("DataBase");

            services
                .AddDbContext<ApplicationDbContext>(
                b => b.UseSqlServer(connectionString
                ));
        }
    }
}
