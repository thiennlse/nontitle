namespace Nontitle_BusinessObject.DTO.OrderCheckDto;

public class OrderCheckResponseDto : OrderCheckRequestDto
{
    public string Id { get; set; } = string.Empty;
    public string? UserId { get; set; }
}

