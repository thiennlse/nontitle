using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Nontitle_BusinessObject.DTO.AuthDto;
using Nontitle_BusinessObject.Enum;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Infrastructure;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nontitle_Service.Extensions;

public class AuthExtension
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthExtension(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<AuthResponseDto> GetAuthKey(ApplicationUser user)
    {
        var provider = _config["JwtSettings:Issuer"]!;
        var refreshToken = await GenerateToken(user, TokenType.REFRESH);
        IdentityResult refreshTokenResutl = await _userManager.SetAuthenticationTokenAsync(user, provider, "RefreshToken", refreshToken);
        if (!refreshTokenResutl.Succeeded)
            throw new ErrorException(StatusCodes.Status500InternalServerError, ApiCodes.INTERNAL_SERVER_ERROR, "Error when set refreshToken");

        var accessToken = await GenerateToken(user, TokenType.ACCESS);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiredTime = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"]!))
        };
    }

    public async Task<string> GeneratePassword(string password)
    {
        return HashPassword(password);
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }
        return sb.ToString();
    }

    private async Task<string> GenerateToken(ApplicationUser user, TokenType type)
    {
        DateTime? expiredAt = null;
        List<Claim>? claims = null;

        if (type == TokenType.REFRESH)
        {
            expiredAt = DateTime.UtcNow.AddDays(double.Parse(_config["JwtSettings:RefreshTokenExpirationDays"]!));
        }
        else if (type == TokenType.ACCESS)
        {
            var roles = await _userManager.GetRolesAsync(user);

            claims = [
            new(JwtRegisteredClaimNames.Sub, user.Id ),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            ..roles.Select(r => new Claim(ClaimTypes.Role, r))
            ];

            expiredAt = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"]!));
        }

        var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
        var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"],
            Expires = expiredAt,
            SigningCredentials = creds
        };

        return new JsonWebTokenHandler().CreateToken(token);
    }
}

