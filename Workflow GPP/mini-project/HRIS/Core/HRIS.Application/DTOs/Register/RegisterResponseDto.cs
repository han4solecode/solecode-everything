using HRIS.Domain.Entity;

namespace HRIS.Application.DTOs.Register
{
    public class RegisterResponseDto : BaseResponseDto
    {
        public Employee EmployeeData { get; set; } = null!;
    }
}