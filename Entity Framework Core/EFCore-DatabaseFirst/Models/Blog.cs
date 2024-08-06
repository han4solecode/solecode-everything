using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCore_DatabaseFirst.Models;

[Table("blogs")]
public partial class Blog
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Blog_Name")]
    public string BlogName { get; set; } = null!;

    [InverseProperty("Blog")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
