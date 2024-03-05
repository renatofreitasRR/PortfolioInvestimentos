using PortfolioInvestimentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Models
{
    public class InvestmentStatement
    {
        public string ProductName { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime Date { get; set; }
    }
}
