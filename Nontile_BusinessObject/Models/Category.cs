using Nontitle_BusinessObject.Base;

namespace Nontitle_BusinessObject.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string>? Images { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}