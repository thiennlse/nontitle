using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nontitle_BusinessObject.DTO.AuthDto;
using Nontitle_BusinessObject.Enum;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Infrastructure;
using Nontitle_Service.Extensions;
using Nontitle_Service.Interfaces;

namespace Nontitle_Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _usermanager;
    private readonly AuthExtension _authExtension;

    public UserService(UserManager<ApplicationUser> usermanager, AuthExtension authExtension)
    {
        _usermanager = usermanager;
        _authExtension = authExtension;
    }

    public async Task<AuthResponseDto> Login(LoginByEmailRequestDto request)
    {
        var userEmail = await _usermanager.FindByEmailAsync(request.Email);

        if (userEmail is null ||
            !await _usermanager.CheckPasswordAsync(userEmail, request.Password))
            throw new ErrorException(StatusCodes.Status401Unauthorized, ApiCodes.UNAUTHORIZED, "Wrong password or email");

        var response = await _authExtension.GetAuthKey(userEmail);

        userEmail.RefreshToken = response.RefreshToken;
        await _usermanager.UpdateAsync(userEmail);

        return response;
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto request)
    {
        var userExisted = await _usermanager.FindByEmailAsync(request.Email);
        if (userExisted is not null)
            throw new ErrorException(StatusCodes.Status400BadRequest, ApiCodes.BAD_REQUEST, "Email has been used");

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
        };

        IdentityResult identityResult = await _usermanager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
            throw new ErrorException(StatusCodes.Status400BadRequest, ApiCodes.BAD_REQUEST, BuildIdentityErrors(identityResult));

        IdentityResult addToRoleResult = await _usermanager.AddToRoleAsync(user, Roles.Staff);
        if (!addToRoleResult.Succeeded)
            throw new ErrorException(StatusCodes.Status400BadRequest, ApiCodes.BAD_REQUEST, BuildIdentityErrors(addToRoleResult));

        return await _authExtension.GetAuthKey(user);

    }

    private static string BuildIdentityErrors(IdentityResult result)
    {
        return string.Join(" | ",
            result.Errors.Select(e => $"{e.Code}: {e.Description}")
        );
    }
}

