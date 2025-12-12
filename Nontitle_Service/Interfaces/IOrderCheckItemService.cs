using Nontitle_BusinessObject.DTO.OrderCheckItemDto;

namespace Nontitle_Service.Interfaces;

public interface IOrderCheckItemService
{
    Task<OrderCheckItemResponseDto> GetOrderCheckItemById(string id);
    Task<OrderCheckItemResponseDto> CreateOrderCheckItem(OrderCheckItemRequestDto orderCheckItemRequestDto);
    Task<OrderCheckItemResponseDto> UpdateOrderCheckItem(string id, OrderCheckItemRequestDto orderCheckItemRequestDto);
    Task<bool> DeleteOrderCheckItem(string id);
}

