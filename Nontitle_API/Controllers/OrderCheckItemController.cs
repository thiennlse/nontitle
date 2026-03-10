using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.OrderCheckItemDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class OrderCheckItemController : ControllerBase
{
    private readonly IOrderCheckItemService _orderCheckItemService;

    public OrderCheckItemController(IOrderCheckItemService orderCheckItemService)
    {
        _orderCheckItemService = orderCheckItemService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderCheckItemById(string id)
    {
        OrderCheckItemResponseDto item = await _orderCheckItemService.GetOrderCheckItemById(id);
        return Ok(new ResponseModel<OrderCheckItemResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, item));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderCheckItem(OrderCheckItemRequestDto request)
    {
        OrderCheckItemResponseDto item = await _orderCheckItemService.CreateOrderCheckItem(request);
        return Ok(new ResponseModel<OrderCheckItemResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, item));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderCheckItem(OrderCheckItemRequestDto request, string id)
    {
        OrderCheckItemResponseDto item = await _orderCheckItemService.UpdateOrderCheckItem(id, request);
        return Ok(new ResponseModel<OrderCheckItemResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, item));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderCheckItem(string id)
    {
        bool result = await _orderCheckItemService.DeleteOrderCheckItem(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK, ApiCodes.SUCCESS, result));
    }
}