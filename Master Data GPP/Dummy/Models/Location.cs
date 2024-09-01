using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Models;

[Table("locations")]
public partial class Location
{
    [Key]
    [Column("locationid")]
    public int Locationid { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Column("deptno")]
    public int? Deptno { get; set; }

    [ForeignKey("Deptno")]
    [InverseProperty("Locations")]
    public virtual Department? DeptnoNavigation { get; set; }
}
