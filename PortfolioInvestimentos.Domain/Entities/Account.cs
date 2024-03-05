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
        
        public (bool isValid, string? message) Deposit(decimal value)
        {
            if (value > 0)
                this.Value += value;
            else
                return (false, "Não é possível acrescentar um valor igual a 0 a conta");

            return (true, null);
        }

        public (bool isValid, string? message) Withdraw(decimal value)
        {
            if (this.Value < value)
                return (false, "Não há valor o suficiente na conta para completar a operação");

            this.Value -= value;

            return (true, null);
        }
    }
}
