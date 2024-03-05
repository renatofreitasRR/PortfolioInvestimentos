using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<InvestmentStatement>> GetExtractTransactions(int userId);
    }
}
