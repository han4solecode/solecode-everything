namespace HRIS.Application.DTOs.Emp
{
    public class EmployeeByDepartmentResponseDto
    {
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateOnly? Dob { get; set; }

        public string? EmploymentType { get; set; }
    }
}