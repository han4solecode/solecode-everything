using HRIS.Domain.Entity;

namespace HRIS.Application.DTOs.Dependent
{
    public class DependentCreateResponseDto : BaseResponseDto
    {
        public EmpDependent? Data { get; set; }
    }
}