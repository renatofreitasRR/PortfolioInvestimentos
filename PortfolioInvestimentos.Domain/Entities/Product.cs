using PortfolioInvestimentos.Domain.Entities.Contracts;
using PortfolioInvestimentos.Domain.Enums;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, ProductType type, decimal value, DateTime dueDate)
        {
            Name = name;
            Type = type;
            Value = value;
            DueDate = dueDate;
            IsActive = true;
        }

        protected Product()
        {

        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public ProductType Type { get; private set; }
        public decimal Value { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsActive { get; private set; }
    }
}
