using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Role;

namespace HRIS.Application.Contracts
{
    public interface IRoleService
    {
        Task<BaseResponseDto> CreateRoleAsyc(string roleName);
        
        Task<BaseResponseDto> UpdateRoleAsync(RoleUpdateRequestDto roleUpdateRequest);

        Task<BaseResponseDto> DeleteRoleAsync(string roleName);

        Task<BaseResponseDto> AssignRoleAsync(RoleAssignRequestDto roleAssignRequest);

        Task<BaseResponseDto> RevokeRoleAsync(RoleAssignRequestDto roleRevokeRequest);
    }
}