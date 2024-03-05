using PortfolioInvestimentos.Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Commands.Accounts
{
    public class DepositAccountCommand : ICommand
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
    }
}
