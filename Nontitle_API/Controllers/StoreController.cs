using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.StoreDto;
using Nontitle_BusinessObject.Enum;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;


[ApiController]
[Authorize(Roles = $"{Roles.StoreOwner},{Roles.Admin}")]
[Route("api/[Controller]")]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreById(string id)
    {

        var store = await _storeService.GetById(id);
        return Ok(new ResponseModel<StoreResponseDto>(StatusCodes.Status200OK,
            ApiCodes.SUCCESS, store));

    }

    [HttpPost]
    public async Task<IActionResult> CreateStore(StoreRequestDto request)
    {

        var store = await _storeService.CreateAsync(request);
        return Ok(new ResponseModel<StoreResponseDto>(StatusCodes.Status200OK,
            ApiCodes.SUCCESS, store));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStore(StoreRequestDto request, string id)
    {
        var store = await _storeService.UpdateAsync(id, request);
        return Ok(new ResponseModel<StoreResponseDto>(StatusCodes.Status200OK,
            ApiCodes.SUCCESS, store));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(string id)
    {
        var result = await _storeService.DeleteAsync(id);
        return Ok(new ResponseModel<bool>(StatusCodes.Status200OK,
            ApiCodes.SUCCESS, result));
    }
}

