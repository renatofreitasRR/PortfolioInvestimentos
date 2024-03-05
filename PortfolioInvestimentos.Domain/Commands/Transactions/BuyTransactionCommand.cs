using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;

namespace PortfolioInvestimentos.Domain.Commands.Transactions
{
    public class BuyTransactionCommand : ICommand
    {
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
