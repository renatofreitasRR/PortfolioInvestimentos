using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Account(int userId, decimal value)
        {
            UserId = userId;
            Value = value;
        }

        protected Account() { }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public decimal Value { get; private set; }
        public IEnumerable<Transaction> Transactions { get; private set; } = new List<Transaction>();
    }
}
