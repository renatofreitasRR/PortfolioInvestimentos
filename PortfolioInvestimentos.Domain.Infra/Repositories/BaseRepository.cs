using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Infra.Context;
using PortfolioInvestimentos.Domain.Repositories;
using System.Linq.Expressions;

namespace PortfolioInvestimentos.Domain.Infra.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context
                .Set<T>()
                .AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context
                .Set<T>()
                .Update(entity);
        }

        public void Delete(T entity)
        {
            _context
                 .Set<T>()
                 .Remove(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            var entityExists = await _context
                .Set<T>()
                .AnyAsync(filter);

            return entityExists;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _context
                .Set<T>()
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllWithParamsAsync(Expression<Func<T, bool>> filter)
        {
            var entities = await _context
                .Set<T>()
                .Where(filter)
                .ToListAsync();

            return entities;
        }

        public async Task<T> GetWithParamsAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await _context
                .Set<T>()
                .Where(filter)
                .FirstOrDefaultAsync();

            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
