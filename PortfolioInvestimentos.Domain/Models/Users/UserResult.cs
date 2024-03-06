using PortfolioInvestimentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Models.Users
{
    public class UserResult
    {
        public string Name { get; set; }
        public UserInvestorProfile? Profile { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
    }
}
