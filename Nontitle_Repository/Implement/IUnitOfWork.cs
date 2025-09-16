namespace Nontitle_Repository.Implement;

public interface IUnitOfWork
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    Task SaveChangeAsync();
}