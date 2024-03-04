using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Commands.Accounts
{
    public class CreateAccountCommand : ICommand
    {
        public int UserId { get; set; }
        public decimal Value { get; set; }
    }
}
