using Nontitle_BusinessObject.Base;

namespace Nontitle_BusinessObject.Models;

public class OrderCheckItem : BaseEntity
{
    public required string ProductId { get; set; }
    public string? OrderCheckId { get; set; }
    public float UnitPrice { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; } = default!;
    public OrderCheck? OrderCheck { get; set; }
}
