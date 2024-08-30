using HRIS.Domain.Entity;

namespace HRIS.Application.DTOs.Register
{
    public class RegisterRequestDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Fname { get; set; } = null!;

        public string Lname { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Ssn { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Sex { get; set; } = null!;

        public string Employmenttype { get; set; } = null!;

        public decimal Salary { get; set; }

        public string Status { get; set; } = null!;

        public int? Deptno { get; set; }

        public int Level { get; set; }

        public string? Supervisorempno { get; set; }

        public List<EmpDependent?> EmpDependent { get; set; } = [];
    }
}