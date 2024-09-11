using HRIS.Application.Persistance;
using HRIS.Domain.Entity.Workflow;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly AppDbContext _context;

        public WorkflowRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        // Workflow CRUD
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

        // WorkflowSequence CRUD
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

        // NextStepRule CRUD
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

        // LeaveRequest CRUD
        public async Task CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests()
        {
            var lr = await _context.LeaveRequests.ToListAsync();

            return lr;
        }

        public async Task<LeaveRequest?> GetLeaveRequestById(int id)
        {
            var lr = await _context.LeaveRequests.FindAsync(id);

            return lr;
        }

        public async Task UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();
        }

        // Process CRUD
        public async Task CreateProcess(Process process)
        {
            await _context.Processes.AddAsync(process);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Process>> GetAllProcesses()
        {
            var p = await _context.Processes.ToListAsync();

            return p;
        }

        public async Task<Process?> GetProcessById(int id)
        {
            var p = await _context.Processes.FindAsync(id);

            return p;
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

        // WorkflowAction CRUD
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