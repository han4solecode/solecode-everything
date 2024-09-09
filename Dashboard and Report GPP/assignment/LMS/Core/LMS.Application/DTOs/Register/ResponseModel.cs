namespace LMS.Application.DTOs.Register
{
    public class ResponseModel
    {
        public string Status { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? Token { get; set; }

        public DateTime? ExpiredOn { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}