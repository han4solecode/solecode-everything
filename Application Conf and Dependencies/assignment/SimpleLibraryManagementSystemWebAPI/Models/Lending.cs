using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleLibraryManagementSystemWebAPI.Models;

[Table("lendings")]
public partial class Lending
{
    [Key]
    [Column("lendingid")]
    public int Lendingid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("bookid")]
    public List<int> Bookid { get; set; } = [];

    [Column("borrowdate")]
    public DateOnly? Borrowdate { get; set; }

    [Column("returndate")]
    public DateOnly? Returndate { get; set; }

    [ForeignKey("Bookid")]
    [InverseProperty("Lendings")]
    public virtual Book? Book { get; set; } = null!;

    [ForeignKey("Userid")]
    [InverseProperty("Lendings")]
    public virtual User? User { get; set; } = null!;
}
