using System.Linq.Expressions;

namespace Journey.DataAccess.Repositories;
public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
}
