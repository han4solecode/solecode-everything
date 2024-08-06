using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCore_DatabaseFirst.Models;

[Table("posts")]
public partial class Post
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("postTitle")]
    public string PostTitle { get; set; } = null!;

    [Column("post Content")]
    public string PostContent { get; set; } = null!;

    [Column("1 PublishedON")]
    public DateTime _1PublishedOn { get; set; }

    [Column("2 DeletedON")]
    public DateTime? _2DeletedOn { get; set; }

    [Column("BlogID")]
    public int BlogId { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("Posts")]
    public virtual Blog Blog { get; set; } = null!;
}
