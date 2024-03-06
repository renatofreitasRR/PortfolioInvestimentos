using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Infra.Context;
using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Models;

namespace PortfolioInvestimentos.Domain.Infra.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InvestmentStatement>> GetExtractTransactions(int userId)
        {
            var transaction = await _context
                .Transactions
                .Include(x => x.Account)
                .Include(x => x.Product)
                .Where(x => x.Account.UserId == userId)
                .AsNoTracking()
                .Select(x => new InvestmentStatement
                {
                    ProductName = x.Product.Name,
                    OperationType = x.OperationType,
                    Date = x.CreatedAt,
                    Quantity = x.Quantity,
                    Value = x.Product.CalculateTotalTransactionValue(x.Quantity)
                })
                .OrderBy(x => x.ProductName)
                .ToListAsync();

            return transaction;
        }

        public async Task<PagedList<InvestmentStatement>> GetExtractTransactions(PaginationParams paginationParams, int userId)
        {
            var transaction = _context
                .Transactions
                .Include(x => x.Account)
                .Include(x => x.Product)
                .Where(x => x.Account.UserId == userId)
                .AsNoTracking()
                .Select(x => new InvestmentStatement
                {
                    ProductName = x.Product.Name,
                    OperationType = x.OperationType,
                    Date = x.CreatedAt,
                    Quantity = x.Quantity,
                    Value = x.Product.CalculateTotalTransactionValue(x.Quantity)
                })
                .OrderBy(x => x.ProductName);

            var pagedInvestiments = PagedList<InvestmentStatement>
                .ToPagedList(transaction, paginationParams.PageNumber, paginationParams.PageSize);

            return pagedInvestiments;
        }
    }
}
