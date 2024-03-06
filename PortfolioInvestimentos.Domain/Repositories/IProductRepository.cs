using PortfolioInvestimentos.Domain.Entities;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetNearDueDateProductsAsync();
    }
}
