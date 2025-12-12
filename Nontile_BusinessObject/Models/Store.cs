using Nontitle_BusinessObject.Base;

namespace Nontitle_BusinessObject.Models;

public class Store : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? Images { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }

    public ICollection<Category>? Categories { get; set; } = new List<Category>();
    public ICollection<UserStoreRole>? Users { get; set; } = new List<UserStoreRole>();
    public ICollection<StoreRole>? Roles { get; set; } = new List<StoreRole>();
}

