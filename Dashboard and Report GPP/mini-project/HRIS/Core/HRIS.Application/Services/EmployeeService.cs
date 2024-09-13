using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Email;
using HRIS.Application.DTOs.Emp;
using HRIS.Application.DTOs.LeaveRequest;
using HRIS.Application.DTOs.Request;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using HRIS.Domain.Entity.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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

        public async Task<IEnumerable<object>> GetEmployeeDistributionPerDepartment()
        {
            var employees = await _userManager.Users.ToListAsync();

            var employeeDistributionPerDepartment = employees.GroupBy(e => e.DeptnoNavigation!.Deptname).Select(g => new
            {
                Department = g.Key,
                Count = g.Count()
            });

            return employeeDistributionPerDepartment;
        }

        public async Task<IEnumerable<object>> GetTop5BestEmployee()
        {
            var top5BestEmployees = await _userManager.Users.OrderByDescending(e => e.Worksons.Sum(w => w.Hoursworked)).Where(e => e.Worksons.Sum(w => w.Hoursworked) != 0).Take(5).Select(x => new
            {
                Name = $"{x.Fname} {x.Lname}",
                WorkingHour = x.Worksons.Sum(w => w.Hoursworked)
            }).ToListAsync();

            return top5BestEmployees;
        }

        public async Task<IEnumerable<object>> GetAverageSalaryPerDepartment()
        {
            var employees = await _userManager.Users.ToListAsync();

            var averageSalaryPerDepartment = employees.GroupBy(e => e.DeptnoNavigation!.Deptname).Select(g => new
            {
                Department = g.Key,
                AverageSalary = g.Average(x => x.Salary)
            });

            return averageSalaryPerDepartment;
        }

        public async Task<IEnumerable<EmployeeByDepartmentResponseDto>> GetEmployeeFilterByDepartment(int currentPage, string department)
        {
            var employees = await _userManager.Users.Skip((currentPage - 1) * 20).Take(20).Where(e => e.DeptnoNavigation!.Deptname == department).Select(e => new EmployeeByDepartmentResponseDto
            {
                Name = $"{e.Fname} {e.Lname}",
                Address = e.Address,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                Dob = e.Dob,
                EmploymentType = e.Employmenttype
            }).ToListAsync();

            return employees;
        }

        public async Task<byte[]> GenerateEmployeeFilterByDepartmentReport(string department)
        {
            var i = 1;
            var page = 1;
            
            var employees = await _userManager.Users.Where(e => e.DeptnoNavigation!.Deptname == department).ToListAsync();
            var a = employees.Select((item, index) => new { index, item });
            var b = a.GroupBy(x => x.index / 20, x => x.item, (key, items) => items);

            var document = new PdfDocument();
            var pdfConfig = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Landscape,
                PageSize = PageSize.A4
            };
            pdfConfig.SetMargins(10);

            foreach (var items in b)
            {
                var htmlContent = string.Empty;

                htmlContent += $"<h1> Employee Report - {department} Department </h1>";

                if (b.Count() > 1)
                {
                    htmlContent += $"Page {page++} of {b.Count()} pages";
                }

                htmlContent += "<table>";
                htmlContent += "<tr><th>No</th><th>Name</th><th>Address</th><th>Phone Number</th><th>Email</th><th>DOB</th><th>Employment Type</th></tr>";

                items.ToList().ForEach(item =>
                {
                    htmlContent += "<tr>";
                    htmlContent += $"<td> {i++} </td>";
                    htmlContent += $"<td> {item.Fname} {item.Lname} </td>";
                    htmlContent += $"<td> {item.Address} </td>";
                    htmlContent += $"<td> {item.PhoneNumber} </td>";
                    htmlContent += $"<td> {item.Email} </td>";
                    htmlContent += $"<td> {string.Format("{0:dddd, d MMMM yyyy}", item.Dob)} </td>";
                    htmlContent += $"<td> {item.Employmenttype} </td>";
                    htmlContent += "</tr>";
                });
                htmlContent += "</table>";

                var cssFile = File.ReadAllText(@"./ReportTemplates/style.css");
                CssData css = PdfGenerator.ParseStyleSheet(cssFile);

                PdfGenerator.AddPdfPages(document, htmlContent, pdfConfig, css);
            }

            var ms = new MemoryStream();
            document.Save(ms, false);
            var bytes = ms.ToArray();

            return bytes;
        }

        public async Task<BaseResponseDto> ReviewRequest(ReviewRequestDto reviewRequest)
        {
            // get username from httpcontextaccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);
            var userRoles = await _userManager.GetRolesAsync(user!);

            // get process next step required role
            var process = await _workflowRepository.GetProcessById(reviewRequest.ProcessId);

            if (process == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "No process available"
                };
            }

            var requiredRoleId = process.CurrentStepIdNavigation.RequiredRoleId;

            if (requiredRoleId == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Process is finished"
                };
            }

            // check for supervisor
            if (userRoles.Contains("Employee Supervisor") && process.RequesterIdNavigation.Supervisorempno != user!.Id)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Not authorized to review this process"
                };
            }

            var requiredRole = await _roleManager.FindByIdAsync(requiredRoleId);

            if (!userRoles.Contains(requiredRole!.Name!))
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Not authorized to review this process"
                };
            }

            // create new workflow action
            var newWorkflowAction = new WorkflowAction()
            {
                ProcessId = process.ProcessId,
                StepId = process.CurrentStepId,
                ActorId = user!.Id,
                Action = reviewRequest.Action,
                ActionDate = DateTime.UtcNow,
                Comment = reviewRequest.Comment,
            };

            await _workflowRepository.UpdateWorkflowAction(newWorkflowAction);

            var nsrNextStepId = process.CurrentStepIdNavigation.CurrentStepIds.Where(nsr => nsr.CurrentStepId == process.CurrentStepId && nsr.ConditionValue == reviewRequest.Action).Select(nsr => nsr.NextStepId).SingleOrDefault();

            // update process
            process.Status = $"{reviewRequest.Action} by {requiredRole.Name}";
            process.CurrentStepId = nsrNextStepId;
            await _workflowRepository.UpdateProcess(process);

            // get other actor emails
            var actions = await _workflowRepository.GetAllWorkflowActions();
            var actorEmails = actions.Where(e => e.ProcessId == process.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();
            // get requester email
            var requesterEmail = process.RequesterIdNavigation.Email;
            // remove requesterEmail from actorEmails so requester only receive the email once
            actorEmails.Remove(requesterEmail);

            // check if there is any next actor available
            // if exist, add actor email to actorEmails
            if (process.CurrentStepIdNavigation.RequiredRoleId != null)
            {
                var nextActorRoleName = process.CurrentStepIdNavigation.RequiredRoleIdNavigation.Name;
                var actors = await _userManager.GetUsersInRoleAsync(nextActorRoleName!);
                var nextActorEmails = actors.Select(a => a.Email).ToList();
                // append nextActorEmails to actorEmails
                actorEmails.AddRange(nextActorEmails);
            }

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Request has been reviewed successfuly"
            };
        }
    }
}