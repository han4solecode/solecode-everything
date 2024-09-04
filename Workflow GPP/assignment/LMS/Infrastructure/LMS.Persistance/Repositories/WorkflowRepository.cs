using LMS.Application.Persistance;
using LMS.Domain.Entities.Workflow;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly AppDbContext _context;

        public WorkflowRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CreateWorkflow(Workflow workflow)
        {
            await _context.Workflows.AddAsync(workflow);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Workflow>> GetAllWorkflows()
        {
            var wf = await _context.Workflows.ToListAsync();

            return wf;
        }

        public async Task<Workflow?> GetWorkflowById(int id)
        {
            var wf = await _context.Workflows.FindAsync(id);

            return wf;
        }

        public async Task UpdateWorkflow(Workflow workflow)
        {
            _context.Workflows.Update(workflow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkflow(Workflow workflow)
        {
            _context.Workflows.Remove(workflow);
            await _context.SaveChangesAsync();
        }

        public async Task CreateWorkflowSequence(WorkflowSequence workflowSequence)
        {
            await _context.WorkflowSequences.AddAsync(workflowSequence);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkflowSequence>> GetAllWorkflowSequences()
        {
            var wfs = await _context.WorkflowSequences.ToListAsync();

            return wfs;
        }

        public async Task<WorkflowSequence?> GetWorkflowSequenceById(int id)
        {
            var wfs = await _context.WorkflowSequences.FindAsync(id);

            return wfs;
        }

        public async Task UpdateWorkflowSequence(WorkflowSequence workflowSequence)
        {
            _context.WorkflowSequences.Update(workflowSequence);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkflowSequence(WorkflowSequence workflowSequence)
        {
            _context.WorkflowSequences.Remove(workflowSequence);
            await _context.SaveChangesAsync();
        }

        public async Task CreateNextStepRule(NextStepRule nextStepRule)

        {
            await _context.NextStepRules.AddAsync(nextStepRule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NextStepRule>> GetAllNextStepRules()
        {
            var nsr = await _context.NextStepRules.ToListAsync();

            return nsr;
        }

        public async Task<NextStepRule?> GetNextStepRuleById(int id)
        {
            var nsr = await _context.NextStepRules.FindAsync(id);

            return nsr;
        }

        public async Task UpdateNextStepRule(NextStepRule nextStepRule)
        {
            _context.NextStepRules.Update(nextStepRule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNextStepRule(NextStepRule nextStepRule)
        {
            _context.NextStepRules.Remove(nextStepRule);
            await _context.SaveChangesAsync();
        }

        public async Task CreateBookRequest(BookRequest bookRequest)
        {
            await _context.BookRequests.AddAsync(bookRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookRequest>> GetAllBookRequests()
        {
            var p = await _context.BookRequests.ToListAsync();

            return p;
        }

        public async Task<BookRequest?> GetBookRequestById(int id)
        {
            var p = await _context.BookRequests.FindAsync(id);

            return p;
        }

        public async Task UpdateBookRequest(BookRequest bookRequest)
        {
            _context.BookRequests.Update(bookRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookRequest(BookRequest bookRequest)
        {
            _context.BookRequests.Remove(bookRequest);
            await _context.SaveChangesAsync();
        }

        public async Task CreateProcess(Process process)
        {
            await _context.Processes.AddAsync(process);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Process>> GetAllProcesses()
        {
            var r = await _context.Processes.ToListAsync();

            return r;
        }

        public async Task<Process?> GetProcessById(int id)
        {
            var r = await _context.Processes.FindAsync(id);

            return r;
        }

        public async Task UpdateProcess(Process process)
        {
            _context.Processes.Update(process);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcess(Process process)
        {
            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();
        }

        public async Task CreateWorkflowAction(WorkflowAction workflowAction)
        {
            await _context.WorkflowActions.AddAsync(workflowAction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkflowAction>> GetAllWorkflowActions()
        {
            var wa = await _context.WorkflowActions.ToListAsync();

            return wa;
        }

        public async Task<WorkflowAction?> GetWorkflowActionById(int id)
        {
            var wa = await _context.WorkflowActions.FindAsync(id);

            return wa;
        }

        public async Task UpdateWorkflowAction(WorkflowAction workflowAction)
        {
            _context.WorkflowActions.Update(workflowAction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkflowAction(WorkflowAction workflowAction)
        {
            _context.WorkflowActions.Remove(workflowAction);
            await _context.SaveChangesAsync();
        }
    }
}