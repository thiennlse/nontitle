using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nontitle_BusinessObject.DTO.ProductDto;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> GetProductById(string id)
    {
        Product? product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Product with id {id} not found");

        ProductResponseDto result = _mapper.Map<ProductResponseDto>(product);
        return result;
    }

    public async Task<ProductResponseDto> CreateProduct(ProductRequestDto productRequestDto)
    {
        try
        {
            Product product = _mapper.Map<Product>(productRequestDto);
            await _unitOfWork.ProductRepository.InsertAsync(product);
            await _unitOfWork.SaveChangeAsync();

            ProductResponseDto response = _mapper.Map<ProductResponseDto>(product);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when create product");
        }
    }

    public async Task<ProductResponseDto> UpdateProduct(string id, ProductRequestDto productRequestDto)
    {
        Product? product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Product with id {id} not found");

        try
        {
            _mapper.Map(productRequestDto, product);
            Product updated = await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangeAsync();

            ProductResponseDto response = _mapper.Map<ProductResponseDto>(updated);
            return response;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when update product");
        }
    }

    public async Task<bool> DeleteProduct(string id)
    {
        Product? product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product is null)
            throw new ErrorException(StatusCodes.Status404NotFound, ApiCodes.NOT_FOUND, $"Product with id {id} not found");

        try
        {
            bool result = await _unitOfWork.ProductRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangeAsync();
            return result;
        }
        catch (ErrorException)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when delete product");
        }
    }
}