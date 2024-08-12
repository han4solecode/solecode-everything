using System;
using System.Collections.Generic;

namespace SimpleLibraryManagementSystemWebAPI.Models;

public partial class Book
{
    public int Bookid { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateOnly Publicationyear { get; set; }

    public string Isbn { get; set; } = null!;
}
