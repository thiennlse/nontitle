using Microsoft.AspNetCore.Identity;

namespace Nontitle_BusinessObject.Models;
public sealed class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }

    public ICollection<OrderCheck> OrderChecks { get; set; } = new List<OrderCheck>();
    public ICollection<UserStoreRole> Stores { get; set; } = new List<UserStoreRole>();
}
