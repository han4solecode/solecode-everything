using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Dependent;
using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IEmpDependentService
    {
        Task<IEnumerable<EmpDependent>> GetAllEmpDependents();

        Task<EmpDependent?> GetEmpDependentById(int id);

        Task<DependentCreateResponseDto> AddNewEmpDependent(EmpDependent empDependent);

        Task<BaseResponseDto> UpdateExistingEmpDependent(int id, EmpDependent inputEmpDependet);

        Task<BaseResponseDto> DeleteEmpDependent(int id);
    }
}