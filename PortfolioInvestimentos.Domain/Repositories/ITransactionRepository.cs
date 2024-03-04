using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
    }
}
