using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Repository;
using Hospital_Management_System.Model;

internal class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HospitalDbContext  _dbContext;
    public GenericRepository( HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext .Set<T>().AddAsync(entity);
    }

    
    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null)
             _dbContext.Set<T>().Remove(entity);
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();

    }

    public IQueryable<T> Query => _dbContext.Set<T>().AsQueryable();
}