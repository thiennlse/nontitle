using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.OrderCheckDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class OrderCheckController : ControllerBase
{
    private readonly IOrderCheckService _orderCheckService;

    public OrderCheckController(IOrderCheckService orderCheckService)
    {
        _orderCheckService = orderCheckService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderCheckById(string id)
    {
        OrderCheckResponseDto orderCheck = await _orderCheckService.GetOrderCheckById(id);
        return Ok(new ResponseModel<OrderCheckResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, orderCheck));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderCheck(OrderCheckRequestDto request)
    {
        OrderCheckResponseDto orderCheck = await _orderCheckService.CreateOrderCheck(request);
        return Ok(new ResponseModel<OrderCheckResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, orderCheck));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderCheck(OrderCheckRequestDto request, string id)
    {
        OrderCheckResponseDto orderCheck = await _orderCheckService.UpdateOrderCheck(id, request);
        return Ok(new ResponseModel<OrderCheckResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, orderCheck));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderCheck(string id)
    {
        bool result = await _orderCheckService.DeleteOrderCheck(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK, ApiCodes.SUCCESS, result));
    }
}