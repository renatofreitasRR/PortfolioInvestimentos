using Microsoft.AspNetCore.Identity;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Authentication
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserPasswordService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            return hashedPassword;
        }

        public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}
