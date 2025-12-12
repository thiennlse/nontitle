using Microsoft.AspNetCore.Mvc;
using Nontitle_BusinessObject.DTO.AuthDto;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Interfaces;

namespace Nontitle_API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount(RegisterRequestDto request)
    {
        var response = await _userService.Register(request);
        return Ok(new ResponseModel<AuthResponseDto>
            (StatusCodes.Status200OK, ApiCodes.SUCCESS, response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAccount(LoginByEmailRequestDto requestDto)
    {
        var response = await _userService.Login(requestDto);
        return Ok(new ResponseModel<AuthResponseDto>
                (StatusCodes.Status200OK, ApiCodes.SUCCESS, response));
    }

}

