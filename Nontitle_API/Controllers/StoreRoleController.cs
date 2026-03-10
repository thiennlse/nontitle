using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.StoreRoleDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class StoreRoleController : ControllerBase
{
    private readonly IStoreRoleService _storeRoleService;

    public StoreRoleController(IStoreRoleService storeRoleService)
    {
        _storeRoleService = storeRoleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreRoleById(string id)
    {
        StoreRoleResponseDto role = await _storeRoleService.GetById(id);
        return Ok(new ResponseModel<StoreRoleResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, role));
    }

    [HttpPost]
    public async Task<IActionResult> CreateStoreRole(StoreRoleRequestDto request)
    {
        StoreRoleResponseDto role = await _storeRoleService.CreateAsync(request);
        return Ok(new ResponseModel<StoreRoleResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, role));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStoreRole(StoreRoleRequestDto request, string id)
    {
        StoreRoleResponseDto role = await _storeRoleService.UpdateAsync(id, request);
        return Ok(new ResponseModel<StoreRoleResponseDto>(StatusCodes.Status200OK, ApiCodes.SUCCESS, role));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoreRole(string id)
    {
        bool result = await _storeRoleService.DeleteAsync(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK, ApiCodes.SUCCESS, result));
    }
}