using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.OrderCheckDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class OrderCheckService : IOrderCheckService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderCheckService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderCheckResponseDto> GetOrderCheckById(string id)
    {
        OrderCheck? orderCheck = await _unitOfWork.OrderCheckRepository.GetByIdAsync(id);
        if (orderCheck is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheck with id {id} not found");

        OrderCheckResponseDto response = _mapper.Map<OrderCheckResponseDto>(orderCheck);
        return response;
    }

    public async Task<OrderCheckResponseDto> CreateOrderCheck(OrderCheckRequestDto orderCheckRequestDto)
    {
        try
        {
            OrderCheck orderCheck = _mapper.Map<OrderCheck>(orderCheckRequestDto);
            await _unitOfWork.OrderCheckRepository.InsertAsync(orderCheck);
            await _unitOfWork.SaveChangeAsync();

            OrderCheckResponseDto response = _mapper.Map<OrderCheckResponseDto>(orderCheck);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when create order check");
        }
    }

    public async Task<OrderCheckResponseDto> UpdateOrderCheck(string id, OrderCheckRequestDto orderCheckRequestDto)
    {
        OrderCheck? orderCheck = await _unitOfWork.OrderCheckRepository.GetByIdAsync(id);
        if (orderCheck is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheck with id {id} not found");

        try
        {
            _mapper.Map(orderCheckRequestDto, orderCheck);
            OrderCheck updated = await _unitOfWork.OrderCheckRepository.UpdateAsync(orderCheck);
            await _unitOfWork.SaveChangeAsync();

            OrderCheckResponseDto response = _mapper.Map<OrderCheckResponseDto>(updated);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when update order check");
        }
    }

    public async Task<bool> DeleteOrderCheck(string id)
    {
        OrderCheck? orderCheck = await _unitOfWork.OrderCheckRepository.GetByIdAsync(id);
        if (orderCheck is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"OrderCheck with id {id} not found");

        try
        {
            bool result = await _unitOfWork.OrderCheckRepository.DeleteAsync(orderCheck);
            await _unitOfWork.SaveChangeAsync();
            return result;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when delete order check");
        }
    }
}