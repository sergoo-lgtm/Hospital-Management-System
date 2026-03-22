namespace Hospital_Management_System.Repository;

public interface IGenericRepository<T>
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
        
    IQueryable<T> Query { get; }

}