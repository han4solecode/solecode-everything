using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Models;

[Table("departments")]
[Index("Mgrempno", Name = "departments_mgrempno_key", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("deptno")]
    public int Deptno { get; set; }

    [Column("deptname")]
    [StringLength(255)]
    public string Deptname { get; set; } = null!;

    [Column("mgrempno")]
    public int? Mgrempno { get; set; }

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [ForeignKey("Mgrempno")]
    [InverseProperty("Department")]
    public virtual Employee? MgrempnoNavigation { get; set; }

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
