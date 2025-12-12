using Nontitle_BusinessObject.DTO.Category;

namespace Nontitle_Service.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDto> GetById(string id);
    Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request);
    Task<CategoryResponseDto> UpdateAsync(CategoryRequestDto request,string id);
    Task<bool> DeleteAsync(string id);
}

