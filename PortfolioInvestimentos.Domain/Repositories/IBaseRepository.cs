using System.Linq.Expressions;

namespace PortfolioInvestimentos.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithParamsAsync(Expression<Func<T, bool>> filter);
        Task<T> GetWithParamsAsync(Expression<Func<T, bool>> filter);
        Task SaveAsync();
    }
}
