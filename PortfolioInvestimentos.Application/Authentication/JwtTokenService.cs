using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using PortfolioInvestimentos.Application.Authentication.Models;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioInvestimentos.Application.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(SecretKeySettings.Secret);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
