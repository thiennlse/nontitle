using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.StoreRoleDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class StoreRoleService : IStoreRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StoreRoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StoreRoleResponseDto> CreateAsync(StoreRoleRequestDto request)
    {
        try
        {
            StoreRole storeRole = _mapper.Map<StoreRole>(request);
            await _unitOfWork.StoreRoleRepository.InsertAsync(storeRole);
            await _unitOfWork.SaveChangeAsync();

            StoreRoleResponseDto response = _mapper.Map<StoreRoleResponseDto>(storeRole);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when create store role");
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        StoreRole? storeRole = await _unitOfWork.StoreRoleRepository.GetByIdAsync(id);
        if (storeRole is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Not found store role with id: {id}");

        try
        {
            bool result = await _unitOfWork.StoreRoleRepository.DeleteAsync(storeRole);
            await _unitOfWork.SaveChangeAsync();
            return result;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when delete store role");
        }
    }

    public async Task<StoreRoleResponseDto> GetById(string id)
    {
        StoreRole? storeRole = await _unitOfWork.StoreRoleRepository.GetByIdAsync(id);
        if (storeRole is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Not found store role with id: {id}");

        StoreRoleResponseDto response = _mapper.Map<StoreRoleResponseDto>(storeRole);
        return response;
    }

    public async Task<StoreRoleResponseDto> UpdateAsync(string id, StoreRoleRequestDto request)
    {
        StoreRole? storeRole = await _unitOfWork.StoreRoleRepository.GetByIdAsync(id);
        if (storeRole is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Not found store role with id: {id}");

        try
        {
            _mapper.Map(request, storeRole);
            StoreRole updated = await _unitOfWork.StoreRoleRepository.UpdateAsync(storeRole);
            await _unitOfWork.SaveChangeAsync();

            StoreRoleResponseDto response = _mapper.Map<StoreRoleResponseDto>(updated);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when update store role");
        }
    }
}