using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using HRIS.Domain.Entity.Workflow;
using Microsoft.AspNetCore.Identity;

namespace HRIS.Domain.Entity
{
    public class Employee : IdentityUser
    {
        // [Key]
        // [Column("empno")]
        // public int Empno { get; set; }

        [Column("fname")]
        [StringLength(255)]
        public string Fname { get; set; } = null!;

        [Column("lname")]
        [StringLength(255)]
        public string Lname { get; set; } = null!;

        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;

        [Column("ssn")]
        [StringLength(255)]
        public string Ssn { get; set; } = null!;

        // [Column("phonenumber")]
        // [StringLength(255)]
        // public string Phonenumber { get; set; } = null!;

        // [Column("email")]
        // [StringLength(255)]
        // public string Email { get; set; } = null!;

        [Column("dob")]
        public DateOnly Dob { get; set; }

        [Column("sex")]
        [StringLength(255)]
        public string Sex { get; set; } = null!;

        // [Column("position")]
        // [StringLength(255)]
        // public string Position { get; set; } = null!;

        [Column("employmenttype")]
        [StringLength(255)]
        public string Employmenttype { get; set; } = null!;

        [Column("salary")]
        public decimal Salary { get; set; }

        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;

        [Column("deactreason")]
        [StringLength(255)]
        public string? Deactreason { get; set; }

        [Column("deactivated_at")]
        public DateOnly? DeactivatedAt { get; set; }

        [Column("deptno")]
        public int? Deptno { get; set; }

        [Column("level")]
        public int Level { get; set; }

        [Column("supervisorempno")]
        public string? Supervisorempno { get; set; }

        [InverseProperty("MgrempnoNavigation")]
        public virtual Department? Department { get; set; }

        [ForeignKey("Deptno")]
        [InverseProperty("Employees")]
        public virtual Department? DeptnoNavigation { get; set; }

        [InverseProperty("EmpnoNavigation")]
        public virtual ICollection<EmpDependent> Empdependents { get; set; } = new List<EmpDependent>();

        [InverseProperty("SupervisorempnoNavigation")]
        public virtual ICollection<Employee> InverseSupervisorempnoNavigation { get; set; } = new List<Employee>();

        [ForeignKey("Supervisorempno")]
        [InverseProperty("InverseSupervisorempnoNavigation")]
        public virtual Employee? SupervisorempnoNavigation { get; set; }

        [InverseProperty("EmpnoNavigation")]
        public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();

        // refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // // foreign key to ApplicationUser entity
        // public string ApplicationUserId { get; set; } = null!;
        // public ApplicationUser ApplicationUser { get; set; } = null!;

        // navigation to LeaveRequest
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = [];

        // navigation to Process
        public virtual ICollection<Process> Processes { get; set; } = [];

        // navigation to WorkflowAction
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; } = [];
    }
}