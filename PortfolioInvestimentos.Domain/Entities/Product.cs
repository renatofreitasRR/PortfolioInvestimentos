using PortfolioInvestimentos.Domain.Entities.Contracts;
using PortfolioInvestimentos.Domain.Enums;

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
