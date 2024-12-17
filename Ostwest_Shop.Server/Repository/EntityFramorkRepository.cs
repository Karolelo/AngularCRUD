using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.Interfaces;

namespace Ostwest_Shop.Server.Repository;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly Microsoft.EntityFrameworkCore.DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(Microsoft.EntityFrameworkCore.DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}