using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<InvestmentStatement>> GetExtractTransactions(int userId);
        Task<PagedList<InvestmentStatement>> GetExtractTransactions(PaginationParams paginationParams, int userId);
    }
}
