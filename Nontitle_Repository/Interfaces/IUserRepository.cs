using Nontitle_BusinessObject.Models;

namespace Nontitle_Repository.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByEmail(string email);
    Task<ApplicationUser?> GetById(string id);
    Task<List<ApplicationUser>?> GetAll();
}

