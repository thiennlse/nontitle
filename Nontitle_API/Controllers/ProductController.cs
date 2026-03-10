using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.ProductDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        ProductResponseDto product = await _productService.GetProductById(id);
        return Ok(new ResponseModel<ProductResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductRequestDto request)
    {
        ProductResponseDto product = await _productService.CreateProduct(request);
        return Ok(new ResponseModel<ProductResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(ProductRequestDto request, string id)
    {
        ProductResponseDto product = await _productService.UpdateProduct(id, request);
        return Ok(new ResponseModel<ProductResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, product));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        bool result = await _productService.DeleteProduct(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK, ApiCodes.SUCCESS, result));
    }
}