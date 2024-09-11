namespace LMS.Application.Contracts
{
    public interface IWorkflowService
    {
        Task<int> GetProcessToReviewCount();

        Task<IEnumerable<object>> GetProcessToReview(); 
    }
}