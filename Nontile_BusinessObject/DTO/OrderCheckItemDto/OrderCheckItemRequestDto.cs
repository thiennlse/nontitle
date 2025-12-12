namespace Nontitle_BusinessObject.DTO.OrderCheckItemDto;

public class OrderCheckItemRequestDto
{
    public required string ProductId { get; set; }
    public string? OrderCheckId { get; set; }
    public float UnitPrice { get; set; }
    public int Quantity { get; set; }
}

