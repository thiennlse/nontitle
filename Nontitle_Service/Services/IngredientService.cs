using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.IngredientDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class IngredientService : IIngredientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IngredientResponseDto> CreateIngredient(IngredientRequestDto ingredientRequestDto)
    {
        try
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientRequestDto);
            await _unitOfWork.IngredientRepository.InsertAsync(ingredient);
            return _mapper.Map<IngredientResponseDto>(ingredient);
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(ex.StatusCode, ex.ErrorDetail);
        }
    }

    public async Task<bool> DeleteIngredient(string id)
    {
        var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);
        if (ingredient is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Ingredient with id {id} not found");

        try
        {
            await _unitOfWork.IngredientRepository.DeleteAsync(ingredient);
            return true;
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(ex.StatusCode, ex.ErrorDetail);
        }
    }

    public async Task<IngredientResponseDto> GetIngredientById(string id)
    {
        var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);
        if (ingredient is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Ingredient with id {id} not found");
        return _mapper.Map<IngredientResponseDto>(ingredient);
    }

    public async Task<IngredientResponseDto> UpdateIngredient(string id, IngredientRequestDto ingredientRequestDto)
    {
        var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);
        if (ingredient is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Ingredient with id {id} not found");

        try
        {
            _mapper.Map(ingredientRequestDto, ingredient);
            await _unitOfWork.IngredientRepository.UpdateAsync(ingredient);
            return _mapper.Map<IngredientResponseDto>(ingredient);
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(ex.StatusCode, ex.ErrorDetail);
        }
    }
}

