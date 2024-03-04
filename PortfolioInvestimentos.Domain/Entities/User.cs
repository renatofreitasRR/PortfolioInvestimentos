using PortfolioInvestimentos.Domain.Entities.Contracts;
using PortfolioInvestimentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class User : BaseEntity
    {
        //CTOR for EF
        protected User()
        {

        }

        public User(string name, UserInvestorProfile? profile, string email, UserRole role)
        {
            Name = name;
            Profile = profile;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            Role = role;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public UserInvestorProfile? Profile { get; private set; }
        public UserRole Role { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public void SetPassword(string password)
        {
            PasswordHash = password;
        }
    }
}
