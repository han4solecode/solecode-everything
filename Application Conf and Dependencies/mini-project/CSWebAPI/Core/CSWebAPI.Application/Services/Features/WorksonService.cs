using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Application.Services.Features
{
    public class WorksonService : IWorksonService
    {
        private readonly IWorksonRepository _worksonRepository;
        private readonly CompanyOptions _options;

        public WorksonService(IWorksonRepository worksonRepository, IOptions<CompanyOptions> options)
        {
            _worksonRepository = worksonRepository;
            _options = options.Value;
        }

        public async Task<bool> AddNewWorkson(Workson workson)
        {
            var totalHoursWorkedInProj = await _worksonRepository.TotalHoursWorkedInProject(workson.Projno);
            var assignedEmpCount = await _worksonRepository.AssignedEmployeeCount(workson.Empno);

            try
            {
                if (totalHoursWorkedInProj < _options.MaxWorkingHours && assignedEmpCount < _options.MaxEmpHandleProject)
                {
                    await _worksonRepository.AddWorkson(workson);
                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteWorkson(int empNo, int projNo)
        {
            var workson = await _worksonRepository.GetWorksonById(empNo, projNo);

            if (workson == null)
            {
                return false;
            }

            await _worksonRepository.DeleteWorkson(workson);
            return true;
        }

        public async Task<IEnumerable<Workson>> GetAllWorksons(int a, int b)
        {
            var worksons = await _worksonRepository.GetAllWorkson(a, b);

            return worksons;
        }

        public async Task<Workson?> GetWorksonById(int empNo, int projNo)
        {
            var workson = await _worksonRepository.GetWorksonById(empNo, projNo);

            return workson;
        }

        public async Task<bool> UpdateExistingWorkson(int empNo, int projNo, Workson inputWorkson)
        {
            var worksonToBeUpdated = await _worksonRepository.GetWorksonById(empNo, projNo);
            var totalHoursWorkedInProj = await _worksonRepository.TotalHoursWorkedInProject(inputWorkson.Projno);
            var assignedEmpCount = await _worksonRepository.AssignedEmployeeCount(inputWorkson.Empno);


            if (worksonToBeUpdated == null)
            {
                return false;
            }

            try
            {
                worksonToBeUpdated.Empno = inputWorkson.Empno;
                worksonToBeUpdated.Projno = inputWorkson.Projno;
                worksonToBeUpdated.Dateworked = inputWorkson.Dateworked;
                worksonToBeUpdated.Hoursworked = inputWorkson.Hoursworked;

                if (totalHoursWorkedInProj < _options.MaxWorkingHours && assignedEmpCount < _options.MaxEmpHandleProject)
                {
                    await _worksonRepository.UpdateWorkson(worksonToBeUpdated);
                    return true;
                }

                return false;

            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}