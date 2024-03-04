using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Infra.Context;

namespace PortfolioInvestimentos.Domain.Infra.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
