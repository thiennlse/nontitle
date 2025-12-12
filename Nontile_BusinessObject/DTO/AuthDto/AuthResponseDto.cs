namespace Nontitle_BusinessObject.DTO.AuthDto;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiredTime { get; set; }
    public string RefreshToken {  get; set; } = string.Empty;
}