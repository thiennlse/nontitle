using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.Category;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request)
    {
        try
        {
            var category = _mapper.Map<Category>(request);
            await _unitOfWork.CategoryRepository.InsertAsync(category);
            await _unitOfWork.SaveChangeAsync();

            var response = _mapper.Map<CategoryResponseDto>(category);
            return response;
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when create a new category");
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Not found category with id: {id}");
        try
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError,
                ApiCodes.INTERNAL_SERVER_ERROR, "Error when delete a category");
        }
    }

    public async Task<CategoryResponseDto> GetById(string id)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> UpdateAsync(CategoryRequestDto request, string id)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new ErrorException(StatusCodes.Status404NotFound,
                ApiCodes.NOT_FOUND, $"Not found category with id: {id}");

        try
        {
            _mapper.Map(request, category);
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CategoryResponseDto>(category);
        }
        catch (ErrorException ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError,
                ApiCodes.INTERNAL_SERVER_ERROR, ex.Message);
        }
    }
}

