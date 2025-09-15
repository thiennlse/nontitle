using Nontitle_BusinessObject.Base;
using Nontitle_BusinessObject.Enum;

namespace Nontitle_BusinessObject.Models;

public class OrderCheck : BaseEntity
{
    public required string ApplicationUserId { get; set; }
    public string CheckCode { get; set; } = string.Empty;
    public float TotalValue { get; set; }
    public OrderCheckType Type { get; set; }

    public ApplicationUser User { get; set; } = default!;
    public ICollection<OrderCheckItem> Items { get; set; } = new List<OrderCheckItem>();
}

