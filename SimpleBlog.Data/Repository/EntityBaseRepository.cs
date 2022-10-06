using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleBlog.Data.Abstract;
using SimpleBlog.Model.Contracts;
using System.Linq.Expressions;

namespace SimpleBlog.Data.Repository;

public class EntityBaseRepository<T> : IEntityBaseRepository<T>
    where T : class, IEntityBase, new()
{
    private BlogContext _context;

    public EntityBaseRepository(BlogContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsEnumerable();
    }

    public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query.AsEnumerable();
    }

    public int Count()
    {
        return _context.Set<T>().Count();
    }

    public T GetSingle(string id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public T GetSingle(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query.Where(predicate).FirstOrDefault();
    }

    public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public void Add(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Deleted;
    }
    public void DeleteWhere(Expression<Func<T, bool>> predicate)
    {
        IEnumerable<T> entites = _context.Set<T>().Where(predicate);

        foreach(var entity in entites)
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }
    }

    public virtual void Commit()
    {
        _context.SaveChanges();
    }
}
