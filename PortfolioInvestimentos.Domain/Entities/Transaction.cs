using PortfolioInvestimentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        protected Transaction() { }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public OperationType OperationType { get; private set; }
        public decimal Value { get; private set; }
    }
}
