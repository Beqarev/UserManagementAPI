using Microsoft.EntityFrameworkCore;
using UM.Core.Application.Interfaces;
using UM.Infrastructure.Persistence.Data;

namespace UM.Infrastructure.Persistence.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    public Repository(DataContext context) => _context = context;
    
    public void Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public TEntity Read(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> ReadAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public IEnumerable<TEntity> Read()
    {
        return _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> ReadAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void Delete(int id)
    {
        _context.Set<TEntity>().Remove(this.Read(id));
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Update(int id, TEntity entity)
    {
        var existing = _context.Set<TEntity>().Find(id);
        _context.Entry(existing).CurrentValues.SetValues(entity);
    }
}
