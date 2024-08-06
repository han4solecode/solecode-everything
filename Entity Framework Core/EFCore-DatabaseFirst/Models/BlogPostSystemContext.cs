using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCore_DatabaseFirst.Models;

public partial class BlogPostSystemContext : DbContext
{
    public BlogPostSystemContext()
    {
    }

    public BlogPostSystemContext(DbContextOptions<BlogPostSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SQLServerDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Blogs");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Posts");

            entity.HasOne(d => d.Blog).WithMany(p => p.Posts).HasConstraintName("FK_Posts_Blogs_BlogId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
