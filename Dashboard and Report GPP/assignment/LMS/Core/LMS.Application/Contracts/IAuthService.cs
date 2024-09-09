using LMS.Application.DTOs.Login;
using LMS.Application.DTOs.Register;

namespace LMS.Application.Contracts
{
    public interface IAuthService
    {
        Task<ResponseModel> SignUpAsync(RegisterModel model);

        Task<ResponseModel> LoginAsync(LoginModel model);

        Task<ResponseModel> LogoutAsync(string userName);

        Task<ResponseModel> CreateRoleAsync(string roleName);

        Task<ResponseModel> RegisterLibraryManager(RegisterModel model);

        Task<ResponseModel> RegisterLibrarian(RegisterModel model);

        Task<ResponseModel> RegisterLibraryUser(RegisterModel model);

        Task<ResponseModel> RefreshAccessToken(string refreshToken);
    }
}