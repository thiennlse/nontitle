using Nontitle_BusinessObject.DTO.OrderCheckDto;

namespace Nontitle_Service.Interfaces;

public interface IOrderCheckService
{
    Task<OrderCheckResponseDto> GetOrderCheckById(string id);
    Task<OrderCheckResponseDto> CreateOrderCheck(OrderCheckRequestDto orderCheckRequestDto);
    Task<OrderCheckResponseDto> UpdateOrderCheck(string id, OrderCheckRequestDto orderCheckRequestDto);
    Task<bool> DeleteOrderCheck(string id);
}

