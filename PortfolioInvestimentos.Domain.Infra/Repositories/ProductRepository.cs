using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;
using PortfolioInvestimentos.Domain.Infra.Context;
using PortfolioInvestimentos.Domain.Models;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetNearDueDateProductsAsync()
        {
            var currentDate = DateTime.UtcNow;
            var afterSevenDays = currentDate.AddDays(7);

            var products = await _context
                .Products
                .Where(x => x.DueDate >= currentDate && x.DueDate <= afterSevenDays)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        public PagedList<Product> GetProductsPaged(PaginationParams paginationParams)
        {
            var products = _context
              .Products
              .AsNoTracking()
              .OrderBy(x => x.Name);

            var pagedProducts = PagedList<Product>
                .ToPagedList(products, paginationParams.PageNumber, paginationParams.PageSize);

            return pagedProducts;
        }
    }
}
