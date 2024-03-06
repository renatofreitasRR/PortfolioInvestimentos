using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PortfolioInvestimentos.Application.Authentication.Models;
using System.Text;

namespace PortfolioInvestimentos.Domain.Api.Configurations
{
    public static class CookiesConfiguration
    {
        public static void AddCookiesConfiguration(this IServiceCollection services)
        {
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKeySettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
