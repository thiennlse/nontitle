using Nontitle_BusinessObject.DTO.StoreRoleDto;

namespace Nontitle_Service.Interfaces;

public interface IStoreRoleService
{
    Task<StoreRoleResponseDto> GetById(string id);
    Task<StoreRoleResponseDto> CreateAsync(StoreRoleRequestDto request);
    Task<StoreRoleResponseDto> UpdateAsync(string id, StoreRoleRequestDto request);
    Task<bool> DeleteAsync(string id);
}