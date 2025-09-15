using Microsoft.AspNetCore.Identity;

namespace Nontitle_BusinessObject.Models;
public sealed class ApplicationUser : IdentityUser
{
    public ICollection<OrderCheck> OrderChecks { get; set; } = new List<OrderCheck>();
}
