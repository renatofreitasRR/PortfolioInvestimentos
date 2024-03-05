using PortfolioInvestimentos.Domain.Commands.Contracts;

namespace PortfolioInvestimentos.Domain.Commands.Accounts
{
    public class WithdrawAccountCommand : ICommand
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
    }
}
