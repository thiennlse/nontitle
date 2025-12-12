using Nontitle_BusinessObject.Enum;

namespace Nontitle_BusinessObject.DTO.IngredientDto;

public class IngredientRequestDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? PricePerUnit { get; set; }
    public UnitOfMeasure? UnitOfMeasure { get; set; }
}

