using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.Context;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Interfaces;

namespace Nontitle_Repository.Repositories;

public class OrderCheckRepository : GenericRepository<OrderCheck>, IOrderCheckRepository
{
    public OrderCheckRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
    {
    }
}

