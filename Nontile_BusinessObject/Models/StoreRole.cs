using Nontitle_BusinessObject.Base;

namespace Nontitle_BusinessObject.Models;

public class StoreRole : BaseEntity
{
    public string? StoreId { get; set; }
    public string? Name { get; set; }
    public List<string>? Permission { get; set; }

    public Store? Store { get; set; }
}

