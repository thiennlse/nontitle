using Nontitle_Repository.Interfaces;

namespace Nontitle_Repository.Implement;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get;}
    public ICategoryRepository CategoryRepository { get;}
    public IIngredientRepository IngredientRepository { get;}
    public IOrderCheckRepository OrderCheckRepository { get;}
    public IOrderCheckItemRepository OrderCheckItemRepository { get;}
    public IProductRepository ProductRepository { get;}
    public IStoreRepository StoreRepository { get;}
    public IStoreRoleRepository StoreRoleRepository { get;}

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    Task SaveChangeAsync();
}