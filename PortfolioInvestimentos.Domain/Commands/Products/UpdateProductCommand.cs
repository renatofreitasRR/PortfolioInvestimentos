using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Enums;

namespace PortfolioInvestimentos.Domain.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public int QuantityAvailable { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; }
    }
}
