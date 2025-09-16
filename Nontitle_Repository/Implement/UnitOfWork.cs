
using Nontitle_BusinessObject.Context;

namespace Nontitle_Repository.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

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

