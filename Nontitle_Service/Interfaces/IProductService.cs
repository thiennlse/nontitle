using Nontitle_BusinessObject.DTO.ProductDto;

namespace Nontitle_Service.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> GetProductById(string id);
    Task<ProductResponseDto> CreateProduct(ProductRequestDto productRequestDto);
    Task<ProductResponseDto> UpdateProduct(string id, ProductRequestDto productRequestDto);
    Task<bool> DeleteProduct(string id);
}

