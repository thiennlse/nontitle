namespace Nontitle_BusinessObject.Models;

public class UserStoreRole
{
    public string? UserId { get; set; }
    public string? StoreId { get; set; }
    public string? StoreRoleId { get; set; }

    public ApplicationUser? User { get; set; }
    public Store? Store { get; set; }
    public StoreRole? StoreRole { get; set; }
}

