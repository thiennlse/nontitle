using Nontitle_BusinessObject.Base;
using Nontitle_BusinessObject.Enum;

namespace Nontitle_BusinessObject.Models;

public class Ingredient : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? PricePerUnit { get; set; }
    public UnitOfMeasure? UnitOfMeasure { get; set; }

    public ICollection<ProductIngredient>? ProductIngredients { get; set; } = new List<ProductIngredient>();
}

