using PortfolioInvestimentos.Domain.Entities.Contracts;
using PortfolioInvestimentos.Domain.Enums;
using System.Linq.Expressions;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, ProductType type, decimal value, int quantity, DateTime dueDate)
        {
            Name = name;
            Type = type;
            Value = value;
            DueDate = dueDate;
            QuantityAvailable = quantity;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        protected Product()
        {

        }

        public string Name { get; private set; }
        public ProductType Type { get; private set; }
        public decimal Value { get; private set; }
        public int QuantityAvailable { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsActive { get; private set; }

        public decimal CalculateTotalTransactionValue(int quantity)
        {
            return this.Value * quantity;
        }

        public (bool isValid, string? message) UpdateQuantityAvailable(int quantity, OperationType operation)
        {
            if (quantity == 0)
                return (false, "Não é possível comprar ou vender um valor 0 de produtos");
            
            if (operation == OperationType.Buy)
                this.QuantityAvailable -= quantity;
            else
                this.QuantityAvailable += quantity;

            return (true, null);
        }

        public void Update(string name, ProductType type, decimal value, int quantity, DateTime dueDate, bool isActive)
        {
            Name = name;
            Type = type;
            Value = value;
            DueDate = dueDate;
            QuantityAvailable = quantity;
            IsActive = true;
        }
    }
}
