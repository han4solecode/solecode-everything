using HRIS.Domain.Entity;

namespace HRIS.Application.DTOs.Worksons
{
    public class WorksonCreateResponseDto : BaseResponseDto
    {
        public Workson? Data { get; set; }
    }
}