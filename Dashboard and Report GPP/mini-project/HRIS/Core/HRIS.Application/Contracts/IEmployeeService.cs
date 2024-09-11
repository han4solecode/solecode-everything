using HRIS.Application.DTOs;
using HRIS.Application.DTOs.LeaveRequest;
using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IEmployeeService
    {
        // admin and hr manager
        Task<IEnumerable<Employee>> GetAllEmployees();

        // admin and hr manager
        Task<Employee?> GetEmployeeById(string id);

        // department manager, employee supervisor, employee
        // if user role == dept manager, get all employee detail in department
        // if user role == employee supervisor, get only supervised emp
        // if user role == employee, get only his/her detail
        Task<IEnumerable<object>> GetEmployeeFilterByRole();

        // create udah ada di register si
        // admin and hr manager
        // Task<bool> AddNewEmployee(Employee employee);

        // admin, hr manager, and employee
        Task<BaseResponseDto> UpdateExistingEmployee(string empNo, Employee inputEmployee);

        // admin and hr manager
        Task<BaseResponseDto> DeleteExistingEmployee(string id);

        // admin and hr manager
        Task<BaseResponseDto> DeactivateEmployee(string id, string? reason);

        // employee
        Task<BaseResponseDto> EmployeeLeaveRequest(LeaveRequestDto leaveRequest);

        // employee supervisor and HR manager
        Task<BaseResponseDto> ApproveLeaveRequest(string empNo);
        Task<BaseResponseDto> RejectLeaveRequest(string empNo);

        Task<IEnumerable<object>> GetEmployeeDistributionPerDepartment();

        Task<IEnumerable<object>> GetTop5BestEmployee();
    }
}