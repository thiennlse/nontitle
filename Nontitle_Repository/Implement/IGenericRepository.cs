using Nontitle_BusinessObject.Base;

namespace Nontitle_Repository.Implement;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<List<T>?> GetAllAsync();
    Task<T> InsertAsync(T entity);
    Task<List<T>> InsertRangeAsync(List<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}

