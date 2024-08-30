using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Worksons;
using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IWorksonService
    {
        Task<IEnumerable<object>> GetAllWorksons();

        Task<object> GetWorksonById(string empNo, int projNo);

        Task<WorksonCreateResponseDto> AddNewWorkson(Workson workson);

        Task<BaseResponseDto> UpdateExistingWorkson(string empNo, int projNo, Workson inputWorkson);

        Task<BaseResponseDto> DeleteWorkson(string empNo, int projNo);
    }
}