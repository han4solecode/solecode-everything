using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services
{
    public interface ICompanyServices
    {
        // a
        Task<IEnumerable<Employee>> FromBRICS();

        // b
        Task<IEnumerable<Employee>> Born8090();

        // c
        Task<IEnumerable<Employee>> FemaleBornAfter90();

        // d
        Task<IEnumerable<Employee>> FemaleManager();

        // g
        Task<IEnumerable<Project>> NoEmpProject();

        // h
        Task<IEnumerable<Object>> ListDeptWithMore10Emp();

        // i
        Task<IEnumerable<Object>> ITDeptEmployees();

        // j
        Task<IEnumerable<Employee>> DueRetireManager();

        // l
        Task<int> FemaleManagerCount();

        // m
        Task<IEnumerable<Project>> ITAndHRProjects();

        // t
        Task<IEnumerable<Object>> ManagerUnder40();
    }
}