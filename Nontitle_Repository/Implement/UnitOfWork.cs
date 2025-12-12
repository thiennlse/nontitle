using Microsoft.Extensions.DependencyInjection;
using Nontitle_BusinessObject.Context;
using Nontitle_Repository.Interfaces;

namespace Nontitle_Repository.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _provider;

    public UnitOfWork(ApplicationDbContext context, IServiceProvider provider)
    {
        _context = context;
        _provider = provider;
    }

    public IUserRepository UserRepository => _provider.GetRequiredService<IUserRepository>();

    public ICategoryRepository CategoryRepository => _provider.GetRequiredService<ICategoryRepository>();

    public IIngredientRepository IngredientRepository => _provider.GetRequiredService<IIngredientRepository>();

    public IOrderCheckRepository OrderCheckRepository => _provider.GetRequiredService<IOrderCheckRepository>();

    public IOrderCheckItemRepository OrderCheckItemRepository => _provider.GetRequiredService<IOrderCheckItemRepository>();

    public IProductRepository ProductRepository => _provider.GetRequiredService<IProductRepository>();

    public IStoreRepository StoreRepository => _provider.GetRequiredService<IStoreRepository>();

    public IStoreRoleRepository StoreRoleRepository => _provider.GetRequiredService<IStoreRoleRepository>();

    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _context.Database.RollbackTransaction();
    }

    public async Task SaveChangeAsync()
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during SaveChangesAsync: {ex.Message}");
                await transaction.RollbackAsync();
            }
        }
    }
}

