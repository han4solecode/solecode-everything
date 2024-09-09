using LMS.Domain.Entities.Workflow;

namespace LMS.Application.Persistance
{
    public interface IWorkflowRepository
    {
        // Workflow CRUD
        Task CreateWorkflow(Workflow workflow);
        Task<IEnumerable<Workflow>> GetAllWorkflows();
        Task<Workflow?> GetWorkflowById(int id);
        Task UpdateWorkflow(Workflow workflow);
        Task DeleteWorkflow(Workflow workflow);

        // WorkflowSequence CRUD
        Task CreateWorkflowSequence(WorkflowSequence workflowSequence);
        Task<IEnumerable<WorkflowSequence>> GetAllWorkflowSequences();
        Task<WorkflowSequence?> GetWorkflowSequenceById(int id);
        Task UpdateWorkflowSequence(WorkflowSequence workflowSequence);
        Task DeleteWorkflowSequence(WorkflowSequence workflowSequence);

        // NextStepRule CRUD
        Task CreateNextStepRule(NextStepRule nextStepRule);
        Task<IEnumerable<NextStepRule>> GetAllNextStepRules();
        Task<NextStepRule?> GetNextStepRuleById(int id);
        Task UpdateNextStepRule(NextStepRule nextStepRule);
        Task DeleteNextStepRule(NextStepRule nextStepRule);

        // Process CRUD
        // Task CreateProcess(Process process);
        // Task<IEnumerable<Process>> GetAllProcesses();
        // Task<Process?> GetProcessById(int id);
        // Task UpdateProcess(Process process);
        // Task DeleteProcess(Process process);

        // BookRequest CRUD
        Task CreateBookRequest(BookRequest bookRequest);
        Task<IEnumerable<BookRequest>> GetAllBookRequests();
        Task<BookRequest?> GetBookRequestById(int id);
        Task UpdateBookRequest(BookRequest bookRequest);
        Task DeleteBookRequest(BookRequest bookRequest);


        // Request CRUD
        // Task CreateRequest(Request request);
        // Task<IEnumerable<Request>> GetAllRequests();
        // Task<Request?> GetRequestById(int id);
        // Task UpdateRequest(Request request);
        // Task DeleteRequest(Request request);

        // Process CRUD
        Task CreateProcess(Process process);
        Task<IEnumerable<Process>> GetAllProcesses();
        Task<Process?> GetProcessById(int id);
        Task UpdateProcess(Process process);
        Task DeleteProcess(Process process);
        
        // WorkflowAction CRUD
        Task CreateWorkflowAction(WorkflowAction workflowAction);
        Task<IEnumerable<WorkflowAction>> GetAllWorkflowActions();
        Task<WorkflowAction?> GetWorkflowActionById(int id);
        Task UpdateWorkflowAction(WorkflowAction workflowAction);
        Task DeleteWorkflowAction(WorkflowAction workflowAction);
    }
}