﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleLibraryManagementSystemWebAPI.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Lending> Lendings { get; set; } = new List<Lending>();
}
