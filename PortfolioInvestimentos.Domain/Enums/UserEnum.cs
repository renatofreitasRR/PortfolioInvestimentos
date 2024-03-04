using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Enums
{
    public enum UserInvestorProfile
    {
        Conservative = 1,
        Moderate,
        Aggressive
    }

    public enum UserRole
    {
        Manager = 1,
        Client
    }
}
