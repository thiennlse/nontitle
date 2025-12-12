using Nontitle_BusinessObject.DTO.IngredientDto;

namespace Nontitle_Service.Interfaces;

public interface IIngredientService
{
    Task<IngredientResponseDto> GetIngredientById(string id);
    Task<IngredientResponseDto> CreateIngredient(IngredientRequestDto ingredientRequestDto);
    Task<IngredientResponseDto> UpdateIngredient(string id, IngredientRequestDto ingredientRequestDto);
    Task<bool> DeleteIngredient(string id);
}

