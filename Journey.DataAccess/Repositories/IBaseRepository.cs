using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Journey.DataAccess.Repositories;
public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync();
    List<TEntity> GetAll();
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
}
