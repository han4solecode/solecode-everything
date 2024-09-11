using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Email;
using HRIS.Application.DTOs.LeaveRequest;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using HRIS.Domain.Entity.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailService _emailService;

        public EmployeeService(UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, IWorkflowRepository workflowRepository, RoleManager<AppRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _workflowRepository = workflowRepository;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<BaseResponseDto> DeactivateEmployee(string id, string? reason)
        {
            var emp = await _userManager.FindByIdAsync(id);

            if (emp == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee does not exist"
                };
            }

            try
            {
                emp.Status = "Not Active";
                emp.DeactivatedAt = DateOnly.FromDateTime(DateTime.Now);
                emp.Deactreason = reason;

                await _userManager.UpdateAsync(emp);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee deactivated successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee cannot be deactivated"
                };
            }
        }

        public async Task<BaseResponseDto> DeleteExistingEmployee(string id)
        {
            var emp = await _userManager.FindByIdAsync(id);

            if (emp == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee does not exist"
                };
            }

            try
            {
                await _userManager.DeleteAsync(emp);
                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee deleted successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee cannot be deleted"
                };
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var emps = await _userManager.Users.Where(e => e.Status == "Active").ToListAsync();

            return emps;
        }

        public async Task<Employee?> GetEmployeeById(string id)
        {
            var emp = await _userManager.FindByIdAsync(id);

            return emp;
        }

        public async Task<IEnumerable<object>> GetEmployeeFilterByRole()
        {
            // get user form httpcontext
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var emp = await _userManager.FindByNameAsync(userName!);

            // var emp = await _userManager.FindByIdAsync(userId);

            var empRoles = await _userManager.GetRolesAsync(emp!);


            if (empRoles.Any(r => r == "Department Manager"))
            {
                var empInDept = await _userManager.Users.Where(e => e.Deptno == emp!.Deptno && e.Status == "Active").Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                    EmploymentType = e.Employmenttype,
                    Level = e.Level,
                    e.Sex,
                    DOB = e.Dob,
                    e.Salary
                }).ToListAsync();

                return empInDept;
            }
            if (empRoles.Any(r => r == "Employee Supervisor"))
            {
                var supervisedEmp = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                    EmploymentType = e.Employmenttype,
                    Level = e.Level,
                    e.Sex,
                    DOB = e.Dob,
                    e.Salary
                }).ToListAsync();

                return supervisedEmp;
            }
            else
            {
                var empDetail = await _userManager.Users.Where(e => e.Id == emp!.Id).Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                    EmploymentType = e.Employmenttype,
                    Level = e.Level,
                    e.Sex,
                    DOB = e.Dob,
                    e.Salary,
                    SSN = e.Ssn
                }).ToListAsync();

                return empDetail;
            }
        }

        public async Task<BaseResponseDto> UpdateExistingEmployee(string empNo, Employee inputEmployee)
        {
            // get user form httpcontext
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            foreach (var empRole in empRoles)
            {
                if (empRole == "Administrator" || empRole == "HR Manager")
                {
                    var empToBeUpdated = await _userManager.FindByIdAsync(empNo);

                    if (empToBeUpdated == null)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "Employee does not exist"
                        };
                    }

                    empToBeUpdated.Address = inputEmployee.Address;
                    empToBeUpdated.Deptno = inputEmployee.Deptno;
                    empToBeUpdated.Dob = inputEmployee.Dob;
                    empToBeUpdated.Email = inputEmployee.Email;
                    empToBeUpdated.Empdependents = inputEmployee.Empdependents;
                    empToBeUpdated.Employmenttype = inputEmployee.Employmenttype;
                    empToBeUpdated.Fname = inputEmployee.Fname;
                    empToBeUpdated.Level = inputEmployee.Level;
                    empToBeUpdated.Lname = inputEmployee.Lname;
                    empToBeUpdated.PhoneNumber = inputEmployee.PhoneNumber;
                    empToBeUpdated.Salary = inputEmployee.Salary;
                    empToBeUpdated.Sex = inputEmployee.Sex;
                    empToBeUpdated.Ssn = inputEmployee.Ssn;

                    await _userManager.UpdateAsync(empToBeUpdated);

                    return new BaseResponseDto
                    {
                        Status = "Success",
                        Message = "Employee updated successfully"
                    };
                }
                else
                {
                    var userId = await _userManager.GetUserIdAsync(emp!);

                    if (userId != empNo)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "You can only update your own profile"
                        };
                    }

                    if (emp!.Ssn != inputEmployee.Ssn || emp.Salary != inputEmployee.Salary)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "Cant update SSN or salary"
                        };
                    }

                    emp!.Address = inputEmployee.Address;
                    emp.Deptno = inputEmployee.Deptno;
                    emp.Dob = inputEmployee.Dob;
                    emp.Email = inputEmployee.Email;
                    emp.Empdependents = inputEmployee.Empdependents;
                    emp.Employmenttype = inputEmployee.Employmenttype;
                    emp.Fname = inputEmployee.Fname;
                    emp.Level = inputEmployee.Level;
                    emp.Lname = inputEmployee.Lname;
                    emp.PhoneNumber = inputEmployee.PhoneNumber;
                    emp.Salary = inputEmployee.Salary;
                    emp.Sex = inputEmployee.Sex;
                    emp.Ssn = inputEmployee.Ssn;

                    await _userManager.UpdateAsync(emp);

                    return new BaseResponseDto
                    {
                        Status = "Success",
                        Message = "Profile updated successfully"
                    };
                }
            }

            return new BaseResponseDto
            {
                Status = "Error",
                Message = "Something went wrong"
            };
        }

        // used by employee
        public async Task<BaseResponseDto> EmployeeLeaveRequest(LeaveRequestDto leaveRequest)
        {
            // leave request start date and end date validation
            if (leaveRequest.StartDate < DateOnly.FromDateTime(DateTime.Now) || leaveRequest.EndDate < DateOnly.FromDateTime(DateTime.Now) || leaveRequest.EndDate < leaveRequest.StartDate)
            {
                return new BaseResponseDto()
                {
                    Status = "Error",
                    Message = "Please check the leave request start date and end date"
                };
            }

            // get emp username from HttpContextAccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            // get empployee object
            var employee = await _userManager.FindByNameAsync(userName!);

            // get leave request workflow id
            var workflows = await _workflowRepository.GetAllWorkflows();
            var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;

            try
            {
                // create new Process
                var newProcess = new Process()
                {
                    WorkflowId = leaveRequestWorkflowId,
                    RequesterId = employee!.Id,
                    RequestType = "Leave",
                    Status = "Pending",
                    RequestDate = DateTime.UtcNow,
                    CurrentStepId = 2
                };

                // save new process to database
                await _workflowRepository.CreateProcess(newProcess);

                // get latest process id with requesterId == employeeId for workflow action
                var processes = await _workflowRepository.GetAllProcesses();
                // var latestProcessId = processes.OrderByDescending(p => p.ProcessId).First().ProcessId;
                var latestProcessId = processes.Where(p => p.RequesterId == employee.Id).OrderByDescending(p => p.ProcessId).First().ProcessId;

                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcessId,
                    StepId = 1,
                    ActorId = employee.Id,
                    Action = "Leave request submited",
                    ActionDate = DateTime.UtcNow,
                    Comment = $"Requesting {leaveRequest.LeaveType}"
                };

                // save new workflow action to database
                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // create new Leave Request
                var newLeaveRequest = new LeaveRequest()
                {
                    EmployeeId = employee.Id,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveType = leaveRequest.LeaveType,
                    Reason = leaveRequest.Reason,
                    ProcessId = latestProcessId
                };

                // save new leave request
                await _workflowRepository.CreateLeaveRequest(newLeaveRequest);

                // get next role from workflow sequence
                var ws = await _workflowRepository.GetWorkflowSequenceById(newProcess.CurrentStepId);
                var nextRoleId = ws!.RequiredRoleId;
                var nextRole = await _roleManager.FindByIdAsync(nextRoleId!);

                // get employee supervisors from nextRole for email notification
                // var supervisors = await _userManager.GetUsersInRoleAsync(nextRole!.Name!);
                // var supervisorEmails = supervisors.Where(s => s.)
                var empSupervisor = await _userManager.Users.Where(s => s.Id == employee.Supervisorempno).SingleAsync();
                var empSupervisorEmail = empSupervisor.Email;

                // send email notification to employee and employee supervisor
                var emailTemplate = File.ReadAllText(@"./EmailTemplates/LeaveRequest.html");

                var emailBody = string.Format(emailTemplate,
                    $"{employee.Fname} {employee.Lname}",
                    employee.Id,
                    employee.Email,
                    employee.DeptnoNavigation!.Deptname,
                    string.Format("{0:dddd, d MMMM yyyy}", leaveRequest.StartDate),
                    string.Format("{0:dddd, d MMMM yyyy}", leaveRequest.EndDate),
                    leaveRequest.LeaveType,
                    leaveRequest.Reason,
                    $"{newProcess.Status}"
                );

                var mail = new EmailModel()
                {
                    EmailToIds = [employee.Email],
                    EmailCCIds = [empSupervisorEmail],
                    EmailSubject = "Leave Request Accepted",
                    EmailBody = emailBody
                };

                await _emailService.SendEmail(mail);

                return new BaseResponseDto()
                {
                    Status = "Success",
                    Message = "Leave request submission successful"
                };

            }
            catch (System.Exception)
            {
                return new BaseResponseDto()
                {
                    Status = "Error",
                    Message = "Leave request submission unsuccessful"
                };
            }
        }

        public async Task<BaseResponseDto> ApproveLeaveRequest(string empNo)
        {
            // get emp username from HttpContextAccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            // get empployee object
            var employee = await _userManager.FindByNameAsync(userName!);
            // get employee roles
            var empRoles = await _userManager.GetRolesAsync(employee!);

            if (empRoles.Any(r => r == "Employee Supervisor"))
            {
                // get employee supervisor AppRole object
                var supervisorRole = await _roleManager.FindByNameAsync("Employee Supervisor");

                if (supervisorRole == null)
                {
                    return new BaseResponseDto()
                    {
                        Status = "Error",
                        Message = "Role not found"
                    };
                }

                // get the latest leave request process that has to be reviewed by supervisor
                var p = await _workflowRepository.GetAllProcesses();
                var latestProcess = p.Where(p => p.Status == "Pending" && p.CurrentStepIdNavigation.RequiredRoleId == supervisorRole.Id && p.RequesterId == empNo).TakeLast(1).SingleOrDefault();
    
                if (latestProcess == null)
                {
                    return new BaseResponseDto()
                    {
                        Status = "Error",
                        Message = $"No leave request to review from employee with ID: {empNo}"
                    };
                }

                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcess.ProcessId,
                    StepId = latestProcess.CurrentStepId,
                    ActorId = employee!.Id,
                    Action = "Leave request approved by Supervisor",
                    ActionDate = DateTime.UtcNow,
                    Comment = $"Request for a {latestProcess.LeaveRequestNavigation!.LeaveType} has been approved"
                };

                // save new workflow action
                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // get NextStepRule's NextStepId where currentStepId == latestProcess.CurrentStepId and ConditionValue == "Approved" from WorkflowSequence
                // get leave request workflowid
                var workflows = await _workflowRepository.GetAllWorkflows();
                var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;
                // get workflow sequence where workflowid == leaveRequestWorkflowId and step order == latestProcess.CurrentStepId
                var ws = await _workflowRepository.GetAllWorkflowSequences();
                var wsLeaveRequest = ws.Where(ws => ws.WorkflowId == leaveRequestWorkflowId && ws.StepId == latestProcess.CurrentStepId).Single();
                // get nsr nextStepId
                var nsrNextStepId = wsLeaveRequest.CurrentStepIds.Where(nsr => nsr.CurrentStepId == latestProcess.CurrentStepId && nsr.ConditionValue == "Approved").Select(nsr => nsr.NextStepId).SingleOrDefault();

                // update process
                latestProcess.Status = "Pending HR Manager Review";
                latestProcess.CurrentStepId = nsrNextStepId;
                await _workflowRepository.UpdateProcess(latestProcess);

                // send email notification to employee and HR manager
                var emailTemplate = File.ReadAllText(@"./EmailTemplates/LeaveRequest.html");

                var emailBody = string.Format(emailTemplate, 
                    $"{latestProcess.RequesterIdNavigation.Fname} {latestProcess.RequesterIdNavigation.Lname}",
                    latestProcess.RequesterIdNavigation.Id,
                    latestProcess.RequesterIdNavigation.Email,
                    latestProcess.RequesterIdNavigation.DeptnoNavigation!.Deptname,
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.StartDate),
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.EndDate),
                    latestProcess.LeaveRequestNavigation.LeaveType,
                    latestProcess.LeaveRequestNavigation.Reason,
                    $"{latestProcess.Status}"
                );

                // get HR Manager email
                var hrManagerRoleName = latestProcess.CurrentStepIdNavigation.RequiredRoleIdNavigation.Name;
                var hrManager = await _userManager.GetUsersInRoleAsync(hrManagerRoleName!);

                var mail = new EmailModel()
                {
                    EmailToIds = [latestProcess.RequesterIdNavigation.Email],
                    EmailCCIds = hrManager.Select(hm => hm.Email).ToList()!,
                    EmailSubject = "Leave Request Approved by Supervisor",
                    EmailBody = emailBody
                };

                await _emailService.SendEmail(mail);

                return new BaseResponseDto()
                {
                    Status = "Success",
                    Message = $"Leave request approved successfully. Request will be reviewed by HR Manager"
                };
            }
            // else if (empRoles.Any(r => r == "HR Manager"))
            else
            {
                // get HR Manager AppRole object
                var hrManagerRole = await _roleManager.FindByNameAsync("HR Manager");

                // get the latest leave request process that has to be reviewed by HR Manager
                var p = await _workflowRepository.GetAllProcesses();
                var latestProcess = p.Where(p => p.Status == "Pending HR Manager Review" && p.CurrentStepIdNavigation.RequiredRoleId == hrManagerRole!.Id && p.RequesterId == empNo).TakeLast(1).Single();

                if (latestProcess == null)
                {
                    return new BaseResponseDto()
                    {
                        Status = "Error",
                        Message = $"No leave request to review from employee with ID: {empNo}"
                    };
                }

                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcess.ProcessId,
                    StepId = latestProcess.CurrentStepId,
                    ActorId = employee!.Id,
                    Action = "Leave request approved by HR Manager",
                    ActionDate = DateTime.UtcNow,
                    Comment = $"Request for a {latestProcess.LeaveRequestNavigation!.LeaveType} has been approved"
                };

                // save new workflow action
                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // get NextStepRule's NextStepId where currentStepId == latestProcess.CurrentStepId and ConditionValue == "Approved" from WorkflowSequence
                // get leave request workflowid
                var workflows = await _workflowRepository.GetAllWorkflows();
                var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;
                // get workflow sequence where workflowid == leaveRequestWorkflowId and step order == latestProcess.CurrentStepId
                var ws = await _workflowRepository.GetAllWorkflowSequences();
                var wsLeaveRequest = ws.Where(ws => ws.WorkflowId == leaveRequestWorkflowId && ws.StepId == latestProcess.CurrentStepId).Single();
                // get nsr nextStepId
                var nsrNextStepId = wsLeaveRequest.CurrentStepIds.Where(nsr => nsr.CurrentStepId == latestProcess.CurrentStepId && nsr.ConditionValue == "Approved").Select(nsr => nsr.NextStepId).Single();

                // update process
                latestProcess.Status = "Leave Request Approved by HR Manager";
                latestProcess.CurrentStepId = nsrNextStepId;
                await _workflowRepository.UpdateProcess(latestProcess);

                // send email notification
                // send email notification to employee and other actor
                var emailTemplate = File.ReadAllText(@"./EmailTemplates/LeaveRequest.html");

                var emailBody = string.Format(emailTemplate, 
                    $"{latestProcess.RequesterIdNavigation.Fname} {latestProcess.RequesterIdNavigation.Lname}",
                    latestProcess.RequesterIdNavigation.Id,
                    latestProcess.RequesterIdNavigation.Email,
                    latestProcess.RequesterIdNavigation.DeptnoNavigation!.Deptname,
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.StartDate),
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.EndDate),
                    latestProcess.LeaveRequestNavigation.LeaveType,
                    latestProcess.LeaveRequestNavigation.Reason,
                    $"{latestProcess.Status}"
                );

                // get other actor emails
                var wa = await _workflowRepository.GetAllWorkflowActions();
                var actorEmails = wa.Where(x => x.ProcessId == latestProcess.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();

                // remove requesterEmail from actorEmails
                var requesterEmail = latestProcess.RequesterIdNavigation.Email;
                actorEmails.Remove(requesterEmail);

                var mail = new EmailModel()
                {
                    EmailToIds = [requesterEmail],
                    EmailCCIds = actorEmails!,
                    EmailSubject = "Leave Request Approved by HR Manager",
                    EmailBody = emailBody
                };

                await _emailService.SendEmail(mail);

                return new BaseResponseDto()
                {
                    Status = "Success",
                    Message = $"Leave request approved successfully"
                };
            }
        }

        public async Task<BaseResponseDto> RejectLeaveRequest(string empNo)
        {
            // get emp username from HttpContextAccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            // get empployee object
            var employee = await _userManager.FindByNameAsync(userName!);
            // get employee roles
            var empRoles = await _userManager.GetRolesAsync(employee!);

            if (empRoles.Any(r => r == "Employee Supervisor"))
            {
                // get employee supervisor AppRole object
                var supervisorRole = await _roleManager.FindByNameAsync("Employee Supervisor");

                // get the latest leave request process that has to be reviewed by supervisor
                var p = await _workflowRepository.GetAllProcesses();
                var latestProcess = p.Where(p => p.Status == "Pending" && p.CurrentStepIdNavigation.RequiredRoleId == supervisorRole!.Id && p.RequesterId == empNo).TakeLast(1).SingleOrDefault();

                if (latestProcess == null)
                {
                    return new BaseResponseDto()
                    {
                        Status = "Error",
                        Message = $"No leave request to review from employee with ID: {empNo}"
                    };
                }
                
                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcess.ProcessId,
                    StepId = latestProcess.CurrentStepId,
                    ActorId = employee!.Id,
                    Action = "Leave request rejected by Supervisor",
                    ActionDate = DateTime.UtcNow,
                    Comment = $"Request for a {latestProcess.LeaveRequestNavigation!.LeaveType} has been rejected"
                };

                // save new workflow action
                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // get NextStepRule's NextStepId where currentStepId == latestProcess.CurrentStepId and ConditionValue == "Rejected" from WorkflowSequence
                // get leave request workflowid
                var workflows = await _workflowRepository.GetAllWorkflows();
                var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;
                // get workflow sequence where workflowid == leaveRequestWorkflowId and step order == latestProcess.CurrentStepId
                var ws = await _workflowRepository.GetAllWorkflowSequences();
                var wsLeaveRequest = ws.Where(ws => ws.WorkflowId == leaveRequestWorkflowId && ws.StepId == latestProcess.CurrentStepId).Single();
                // get nsr nextStepId
                var nsrNextStepId = wsLeaveRequest.CurrentStepIds.Where(nsr => nsr.CurrentStepId == latestProcess.CurrentStepId && nsr.ConditionValue == "Rejected").Select(nsr => nsr.NextStepId).Single();

                // update process
                latestProcess.Status = "Leave Request Rejected by Supervisor";
                latestProcess.CurrentStepId = nsrNextStepId;
                await _workflowRepository.UpdateProcess(latestProcess);

                // send email notification
                // to requester employee and other actor
                var emailTemplate = File.ReadAllText(@"./EmailTemplates/LeaveRequest.html");

                var emailBody = string.Format(emailTemplate, 
                    $"{latestProcess.RequesterIdNavigation.Fname} {latestProcess.RequesterIdNavigation.Lname}",
                    latestProcess.RequesterIdNavigation.Id,
                    latestProcess.RequesterIdNavigation.Email,
                    latestProcess.RequesterIdNavigation.DeptnoNavigation!.Deptname,
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.StartDate),
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.EndDate),
                    latestProcess.LeaveRequestNavigation.LeaveType,
                    latestProcess.LeaveRequestNavigation.Reason,
                    $"{latestProcess.Status}"
                );

                // get other actor emails
                var wa = await _workflowRepository.GetAllWorkflowActions();
                var actorEmails = wa.Where(x => x.ProcessId == latestProcess.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();

                // remove requesterEmail from actorEmails
                var requesterEmail = latestProcess.RequesterIdNavigation.Email;
                actorEmails.Remove(requesterEmail);

                var mail = new EmailModel()
                {
                    EmailToIds = [requesterEmail],
                    EmailCCIds = actorEmails!,
                    EmailSubject = "Leave Request Rejected by Supervisor",
                    EmailBody = emailBody
                };

                return new BaseResponseDto()
                {
                    Status = "Success",
                    Message = $"Leave request rejected successfully"
                };
            }
            else
            {
                // get HR Manager AppRole object
                var hrManagerRole = await _roleManager.FindByNameAsync("HR Manager");

                // get the latest leave request process that has to be reviewed by HR Manager
                var p = await _workflowRepository.GetAllProcesses();
                var latestProcess = p.Where(p => p.Status == "Pending HR Manager Review" && p.CurrentStepIdNavigation.RequiredRoleId == hrManagerRole!.Id && p.RequesterId == empNo).TakeLast(1).Single();

                if (latestProcess == null)
                {
                    return new BaseResponseDto()
                    {
                        Status = "Error",
                        Message = $"No leave request to review from employee with ID: {empNo}"
                    };
                }

                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcess.ProcessId,
                    StepId = latestProcess.CurrentStepId,
                    ActorId = employee!.Id,
                    Action = "Leave request rejected by HR Manager",
                    ActionDate = DateTime.UtcNow,
                    Comment = $"Request for a {latestProcess.LeaveRequestNavigation!.LeaveType} has been rejected"
                };

                // save new workflow action
                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // get NextStepRule's NextStepId where currentStepId == latestProcess.CurrentStepId and ConditionValue == "Rejected" from WorkflowSequence
                // get leave request workflowid
                var workflows = await _workflowRepository.GetAllWorkflows();
                var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;
                // get workflow sequence where workflowid == leaveRequestWorkflowId and step order == latestProcess.CurrentStepId
                var ws = await _workflowRepository.GetAllWorkflowSequences();
                var wsLeaveRequest = ws.Where(ws => ws.WorkflowId == leaveRequestWorkflowId && ws.StepId == latestProcess.CurrentStepId).Single();
                // get nsr nextStepId
                var nsrNextStepId = wsLeaveRequest.CurrentStepIds.Where(nsr => nsr.CurrentStepId == latestProcess.CurrentStepId && nsr.ConditionValue == "Rejected").Select(nsr => nsr.NextStepId).Single();

                // update process
                latestProcess.Status = "Leave Request Rejected by HR Manager";
                latestProcess.CurrentStepId = nsrNextStepId;
                await _workflowRepository.UpdateProcess(latestProcess);

                // send email notification
                // to employee requester and other actor
                var emailTemplate = File.ReadAllText(@"./EmailTemplates/LeaveRequest.html");

                var emailBody = string.Format(emailTemplate, 
                    $"{latestProcess.RequesterIdNavigation.Fname} {latestProcess.RequesterIdNavigation.Lname}",
                    latestProcess.RequesterIdNavigation.Id,
                    latestProcess.RequesterIdNavigation.Email,
                    latestProcess.RequesterIdNavigation.DeptnoNavigation!.Deptname,
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.StartDate),
                    string.Format("{0:dddd, d MMMM yyyy}", latestProcess.LeaveRequestNavigation.EndDate),
                    latestProcess.LeaveRequestNavigation.LeaveType,
                    latestProcess.LeaveRequestNavigation.Reason,
                    $"{latestProcess.Status}"
                );

                // get other actor emails
                var wa = await _workflowRepository.GetAllWorkflowActions();
                var actorEmails = wa.Where(x => x.ProcessId == latestProcess.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();

                // remove requesterEmail from actorEmails
                var requesterEmail = latestProcess.RequesterIdNavigation.Email;
                actorEmails.Remove(requesterEmail);

                var mail = new EmailModel()
                {
                    EmailToIds = [requesterEmail],
                    EmailCCIds = actorEmails!,
                    EmailSubject = "Leave Request Rejected by HR Manager",
                    EmailBody = emailBody
                };

                return new BaseResponseDto()
                {
                    Status = "Success",
                    Message = $"Leave request rejected successfully"
                };
            }
        }

        public async Task<IEnumerable<object>> GetEmployeeDistributionPerDepartment()
        {
            var employees = await _userManager.Users.ToListAsync();
            var totalEmployeeCount = employees.Count;

            var employeeDistributionPerDepartment = employees.GroupBy(e => e.DeptnoNavigation!.Deptname).Select(g => new {
                Department = g.Key,
                Percentage = (int)Math.Round((double)(g.Count() * 100 / totalEmployeeCount))
                // Percentage = g.Count() / totalEmployeeCount * 100
                // Percentage = (g.Count() / totalEmployeeCount).ToString("0.00")
                // Percentage = (int)(0.5f + ((100f * g.Count()) / totalEmployeeCount))
            });

            return employeeDistributionPerDepartment;
        }

        public async Task<IEnumerable<object>> GetTop5BestEmployee()
        {
            var top5BestEmployees = await _userManager.Users.OrderByDescending(e => e.Worksons.Sum(w => w.Hoursworked)).Where(e => e.Worksons.Sum(w => w.Hoursworked) != 0).Take(5).Select(x => new {
                Name = $"{x.Fname} {x.Lname}",
                WorkingHour = x.Worksons.Sum(w => w.Hoursworked)
            }).ToListAsync();

            return top5BestEmployees;
        }

        public async Task<IEnumerable<object>> GetAverageSalaryPerDepartment()
        {
            var employees = await _userManager.Users.ToListAsync();
            
            var averageSalaryPerDepartment = employees.GroupBy(e => e.DeptnoNavigation!.Deptname).Select(g => new {
                Department = g.Key,
                AverageSalary = g.Average(x => x.Salary)
            });

            return averageSalaryPerDepartment;
        }

        // private async Task<BaseResponseDto> HandleLeaveRequest(LeaveRequestDto leaveRequest)
        // {
        //     // get emp username from HttpContextAccessor
        //     var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        //     // get empployee object
        //     var employee = await _userManager.FindByNameAsync(userName!);

        //     // get leave request workflow id
        //     var workflows = await _workflowRepository.GetAllWorkflows();
        //     var leaveRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Leave Request").Single().WorkflowId;

        //     try
        //     {

        //     }
        //     catch (System.Exception)
        //     {
        //         return new BaseResponseDto()
        //         {
        //             Status = "Error",
        //             Message = "Leave request submission unsuccessful"
        //         };
        //     }
        // }
    }
}