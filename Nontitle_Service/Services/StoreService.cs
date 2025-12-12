using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.StoreDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class StoreService : IStoreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StoreResponseDto> CreateAsync(StoreRequestDto request)
    {
        var store = _mapper.Map<Store>(request);
        try
        {
            await _unitOfWork.StoreRepository.InsertAsync(store);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<StoreResponseDto>(store);
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError,
                ApiCodes.INTERNAL_SERVER_ERROR,
                "Error when create store");
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
        if (store is null)
            throw new ErrorException(StatusCodes.Status404NotFound,
                ApiCodes.NOT_FOUND,
                $"Not found store with id: {id}");
        try
        {
            var response = await _unitOfWork.StoreRepository.DeleteAsync(store);
            return response;
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError,
                ApiCodes.INTERNAL_SERVER_ERROR,
                "Error when delete store");
        }
    }

    public async Task<StoreResponseDto> GetById(string id)
    {
        var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
        if (store is null)
            throw new ErrorException(StatusCodes.Status404NotFound,
                ApiCodes.NOT_FOUND,
                $"Not found store with id: {id}");
        return _mapper.Map<StoreResponseDto>(store);
    }

    public async Task<StoreResponseDto> UpdateAsync(string id, StoreRequestDto request)
    {
        var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
        if (store is null)
            throw new ErrorException(StatusCodes.Status404NotFound,
                ApiCodes.NOT_FOUND,
                $"Not found store with id: {id}");
        try
        {
            _mapper.Map(request, store);
            var response = await _unitOfWork.StoreRepository.UpdateAsync(store);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<StoreResponseDto>(response);
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError,
                ApiCodes.INTERNAL_SERVER_ERROR,
                "Error when update store");
        }
    }
}

