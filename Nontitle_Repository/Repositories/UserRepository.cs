using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Context;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Interfaces;

namespace Nontitle_Repository.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _accessor;

    public UserRepository(ApplicationDbContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }

    public async Task<List<ApplicationUser>?> GetAll()
    {
        var userList = await _context.Users.ToListAsync();
        return userList;
    }

    public async Task<ApplicationUser?> GetById(string id)
    {
        var user = await _context.Users
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> GetCurrentUser()
    {
        var userId = _accessor.HttpContext?.User.FindFirst("sub")?.Value.ToString();

        var user = await _context.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        return user;
    }
}

