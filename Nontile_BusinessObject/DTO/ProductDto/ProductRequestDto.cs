namespace Nontitle_BusinessObject.DTO.ProductDto;

public class ProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new List<string>();
    public float BasePrice { get; set; }
    public float SellPricePerUnit { get; set; }
    public int ReturnRate { get; set; }
    public required string CategoryId { get; set; }
    public List<string> IngredientIds { get; set; } = new List<string>();
}

