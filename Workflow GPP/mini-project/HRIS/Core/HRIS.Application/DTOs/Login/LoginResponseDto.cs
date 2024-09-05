namespace HRIS.Application.DTOs.Login
{
    public class LoginResponseDto : BaseResponseDto
    {
        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpiryTime { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}