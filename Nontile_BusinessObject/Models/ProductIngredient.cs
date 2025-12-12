

namespace Nontitle_BusinessObject.Models;

public class ProductIngredient
{
    public string? IngredientId { get; set; }
    public string? ProductId { get; set; }

    public float? Quantity { get; set; }

    public Ingredient? Ingredient { get; set; }
    public Product? Product { get; set; }
}

