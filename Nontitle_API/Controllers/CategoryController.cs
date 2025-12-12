using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.Category;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(string id)
    {

        var category = await _categoryService.GetById(id);
        return Ok(new ResponseModel<CategoryResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
    {

        var category = await _categoryService.CreateAsync(request);
        return Ok(new ResponseModel<CategoryResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, category));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(CategoryRequestDto request, string id)
    {
        var category = await _categoryService.UpdateAsync(request, id);
        return Ok(new ResponseModel<CategoryResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, category));

    }
}

