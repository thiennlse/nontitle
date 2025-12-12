using Nontitle_BusinessObject.Enum;

namespace Nontitle_BusinessObject.DTO.OrderCheckDto;

public class OrderCheckRequestDto
{
    public string CheckCode { get; set; } = string.Empty;
    public float TotalValue { get; set; }
    public OrderCheckType Type { get; set; }
}
