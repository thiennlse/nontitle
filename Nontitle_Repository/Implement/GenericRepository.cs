
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Base;
using Nontitle_BusinessObject.Context;
using System.Threading.Tasks;

namespace Nontitle_Repository.Implement;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly IHttpContextAccessor _accessor;

    public GenericRepository(ApplicationDbContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _accessor = accessor;
    }

    private string GetCurrentUsername()
    {
        var currUsername = _accessor.HttpContext?.User?.FindFirst("username")?.Value ?? "System";

        return currUsername;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedBy = GetCurrentUsername();
        entity.IsDeleted = true;

        await UpdateAsync(entity);
        return true;
    }

    public async Task<List<T>?> GetAllAsync()
    {
        var listEntity = await _dbSet.ToListAsync();
        return listEntity;
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var entity = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<T> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<List<T>> InsertRangeAsync(List<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        entity.CreatedBy = GetCurrentUsername();

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}

