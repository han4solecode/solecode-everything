using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Models;

[Table("empdependents")]
public partial class Empdependent
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("dob")]
    public DateOnly Dob { get; set; }

    [Column("sex")]
    [StringLength(255)]
    public string Sex { get; set; } = null!;

    [Column("relationship", TypeName = "character varying")]
    public string Relationship { get; set; } = null!;

    [Column("empno")]
    public int? Empno { get; set; }

    [ForeignKey("Empno")]
    [InverseProperty("Empdependents")]
    public virtual Employee? EmpnoNavigation { get; set; }
}
