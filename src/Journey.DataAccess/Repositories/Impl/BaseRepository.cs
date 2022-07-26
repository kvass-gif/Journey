using Journey.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Journey.DataAccess.Repositories.Impl;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly IdentityDbContext<ApplicationUser> context;
    protected readonly DbSet<TEntity> dbSet;
    public BaseRepository(IdentityDbContext<ApplicationUser> context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await dbSet.AddAsync(entity)).Entity;
        return addedEntity;
    }
    public TEntity Delete(TEntity entity)
    {
        var removedEntity = dbSet.Remove(entity).Entity;
        return removedEntity;
    }
    public TEntity Update(TEntity entity)
    {
        dbSet.Update(entity);
        return entity;
    }
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
    public List<TEntity> GetAll()
    {
        return dbSet.ToList();
    }
    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await dbSet.Where(predicate).FirstOrDefaultAsync();
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        return entity;
    }

}
