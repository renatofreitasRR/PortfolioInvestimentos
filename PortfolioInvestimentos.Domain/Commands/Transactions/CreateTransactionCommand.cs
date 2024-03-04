using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;

namespace PortfolioInvestimentos.Domain.Commands.Transactions
{
    public class CreateTransactionCommand : ICommand
    {
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public OperationType OperationType { get; set; }
        public decimal Value { get; set; }
    }
}
