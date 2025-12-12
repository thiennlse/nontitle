using Nontitle_BusinessObject.DTO.AuthDto;
using Nontitle_BusinessObject.Models;

namespace Nontitle_Service.Interfaces;

public interface IUserService
{
    public Task<AuthResponseDto> Register(RegisterRequestDto request);
    public Task<AuthResponseDto> Login(LoginByEmailRequestDto request);
}

