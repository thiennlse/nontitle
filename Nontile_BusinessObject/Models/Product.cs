using Nontitle_BusinessObject.Base;

namespace Nontitle_BusinessObject.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new List<string>();
    public float BasePrice { get; set; }
    public float SellPricePerUnit { get; set; }
    public int ReturnRate { get; set; }
    public required string CategoryId { get; set; }


    public Category Category { get; set; } = default!;
    public ICollection<ProductIngredient>? ProductIngredients { get; set; } = new List<ProductIngredient>();
}