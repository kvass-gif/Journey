using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Journey.DataAccess.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly IdentityDbContext<IdentityUser> context;
    protected readonly DbSet<TEntity> DbSet;
    public BaseRepository(IdentityDbContext<IdentityUser> context)
    {
        this.context = context;
        DbSet = context.Set<TEntity>();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        return addedEntity;
    }
    public TEntity Delete(TEntity entity)
    {
        var removedEntity = DbSet.Remove(entity).Entity;
        return removedEntity;
    }
    public TEntity Update(TEntity entity)
    {
        DbSet.Update(entity);
        return entity;
    }
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }
    public List<TEntity> GetAll()
    {
        return DbSet.ToList();
    }
    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbSet.Where(predicate).FirstOrDefaultAsync();
        if (entity == null) 
        {
            throw new NullReferenceException();
        }
        return entity;
    }
    
}
