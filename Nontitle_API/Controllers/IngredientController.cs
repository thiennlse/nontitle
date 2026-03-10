using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.IngredientDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredientById(string id)
    {
        IngredientResponseDto ingredient = await _ingredientService.GetIngredientById(id);
        return Ok(new ResponseModel<IngredientResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, ingredient));
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredient(IngredientRequestDto request)
    {
        IngredientResponseDto ingredient = await _ingredientService.CreateIngredient(request);
        return Ok(new ResponseModel<IngredientResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, ingredient));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient(IngredientRequestDto request, string id)
    {
        IngredientResponseDto ingredient = await _ingredientService.UpdateIngredient(id, request);
        return Ok(new ResponseModel<IngredientResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, ingredient));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(string id)
    {
        bool result = await _ingredientService.DeleteIngredient(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK, ApiCodes.SUCCESS, result));
    }
}