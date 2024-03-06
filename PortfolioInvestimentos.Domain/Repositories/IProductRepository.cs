using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetNearDueDateProductsAsync();
        PagedList<Product> GetProductsPaged(PaginationParams paginationParams);
    }
}
