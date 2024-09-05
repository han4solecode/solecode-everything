using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Login;
using HRIS.Application.DTOs.RefreshToken;
using HRIS.Application.DTOs.Register;

namespace HRIS.Application.Contracts
{
    public interface IAuthService
    {
        // Task<BaseResponseDto> CreateRoleAsyc(string roleName);

        Task<RegisterResponseDto> RegisterEmployeeAsync(RegisterRequestDto registerRequest, string roleName);

        Task<RefreshTokenResponseDto> RefreshAccessTokenAsync(string refreshToken);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    }
}