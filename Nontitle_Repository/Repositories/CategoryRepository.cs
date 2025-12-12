using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Context;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Interfaces;

namespace Nontitle_Repository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
    {
    }
}

