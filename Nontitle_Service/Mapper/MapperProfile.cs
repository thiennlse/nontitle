using AutoMapper;
using Nontitle_BusinessObject.DTO.Category;
using Nontitle_BusinessObject.DTO.IngredientDto;
using Nontitle_BusinessObject.DTO.OrderCheckDto;
using Nontitle_BusinessObject.DTO.OrderCheckItemDto;
using Nontitle_BusinessObject.DTO.ProductDto;
using Nontitle_BusinessObject.DTO.StoreDto;
using Nontitle_BusinessObject.Models;

namespace Nontitle_Service.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryRequestDto>().ReverseMap();
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(dest => dest.CategoryId, src => src.MapFrom(c => c.Id))
            .ReverseMap();

        CreateMap<Ingredient, IngredientRequestDto>().ReverseMap();
        CreateMap<Ingredient, IngredientResponseDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(i => i.Id))
            .ReverseMap();

        CreateMap<OrderCheck, OrderCheckRequestDto>().ReverseMap();
        CreateMap<OrderCheck, OrderCheckResponseDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(o => o.Id))
            .ReverseMap();

        CreateMap<OrderCheckItem, OrderCheckItemRequestDto>().ReverseMap();
        CreateMap<OrderCheckItem, OrderCheckItemResponseDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(oci => oci.Id))
            .ReverseMap();

        CreateMap<Product, ProductRequestDto>().ReverseMap();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(p => p.Id))
            .ReverseMap();

        CreateMap<Store, StoreRequestDto>().ReverseMap();
        CreateMap<Store, StoreResponseDto>().
            ForMember(dest => dest.StoreId, src => src.MapFrom(s => s.Id))
            .ReverseMap();
    }
}

