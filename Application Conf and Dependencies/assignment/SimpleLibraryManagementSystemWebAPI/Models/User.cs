using System;
using System.Collections.Generic;

namespace SimpleLibraryManagementSystemWebAPI.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }
}
