using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.OrderCheckItemDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class OrderCheckItemService : IOrderCheckItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderCheckItemService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderCheckItemResponseDto> GetOrderCheckItemById(string id)
    {
        OrderCheckItem? item = await _unitOfWork.OrderCheckItemRepository.GetByIdAsync(id);
        if (item is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheckItem with id {id} not found");

        OrderCheckItemResponseDto response = _mapper.Map<OrderCheckItemResponseDto>(item);
        return response;
    }

    public async Task<OrderCheckItemResponseDto> CreateOrderCheckItem(OrderCheckItemRequestDto orderCheckItemRequestDto)
    {
        try
        {
            OrderCheckItem item = _mapper.Map<OrderCheckItem>(orderCheckItemRequestDto);
            await _unitOfWork.OrderCheckItemRepository.InsertAsync(item);
            await _unitOfWork.SaveChangeAsync();

            OrderCheckItemResponseDto response = _mapper.Map<OrderCheckItemResponseDto>(item);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when create order check item");
        }
    }

    public async Task<OrderCheckItemResponseDto> UpdateOrderCheckItem(string id, OrderCheckItemRequestDto orderCheckItemRequestDto)
    {
        OrderCheckItem? item = await _unitOfWork.OrderCheckItemRepository.GetByIdAsync(id);
        if (item is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheckItem with id {id} not found");

        try
        {
            _mapper.Map(orderCheckItemRequestDto, item);
            OrderCheckItem updated = await _unitOfWork.OrderCheckItemRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangeAsync();

            OrderCheckItemResponseDto response = _mapper.Map<OrderCheckItemResponseDto>(updated);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when update order check item");
        }
    }

    public async Task<bool> DeleteOrderCheckItem(string id)
    {
        OrderCheckItem? item = await _unitOfWork.OrderCheckItemRepository.GetByIdAsync(id);
        if (item is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheckItem with id {id} not found");

        try
        {
            bool result = await _unitOfWork.OrderCheckItemRepository.DeleteAsync(item);
            await _unitOfWork.SaveChangeAsync();
            return result;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when delete order check item");
        }
    }
}