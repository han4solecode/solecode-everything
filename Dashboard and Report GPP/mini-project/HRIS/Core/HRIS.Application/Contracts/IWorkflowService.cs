using HRIS.Application.DTOs.Workflow;

namespace HRIS.Application.Contracts
{
    public interface IWorkflowService
    {
        Task<IEnumerable<object>> GetProcessToReview();

        Task<IEnumerable<TotalLeavesResponseDto>> GetTotalLeavesTakenPerLeaveType(DateOnly startDate, DateOnly endDate);

        Task<byte[]> GenerateTotalLeavesTakenPerLeaveTypeReport(DateOnly startDate, DateOnly endDate);
    }
}