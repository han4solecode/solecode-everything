using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleLibraryManagementSystemWebAPI.Models;

[Table("books")]
public partial class Book
{
    [Key]
    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("author")]
    [StringLength(255)]
    public string Author { get; set; } = null!;

    [Column("publicationyear")]
    public DateOnly Publicationyear { get; set; }

    [Column("isbn")]
    [StringLength(17)]
    public string Isbn { get; set; } = null!;

    [InverseProperty("Book")]
    public virtual ICollection<Lending> Lendings { get; set; } = new List<Lending>();
}
