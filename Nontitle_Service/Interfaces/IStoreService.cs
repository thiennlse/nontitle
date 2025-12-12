using Nontitle_BusinessObject.DTO.StoreDto;

namespace Nontitle_Service.Interfaces;
public interface IStoreService
{
    Task<StoreResponseDto> GetById(string id);
    Task<StoreResponseDto> CreateAsync(StoreRequestDto request);
    Task<StoreResponseDto> UpdateAsync(string id, StoreRequestDto request);
    Task<bool> DeleteAsync(string id);
}
