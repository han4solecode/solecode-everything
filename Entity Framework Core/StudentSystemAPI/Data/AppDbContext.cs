﻿using Microsoft.EntityFrameworkCore;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
    }
}
