using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Models;

public partial class HrisContext : DbContext
{
    public HrisContext()
    {
    }

    public HrisContext(DbContextOptions<HrisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Empdependent> Empdependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Workson> Worksons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Deptno).HasName("departments_pkey");

            entity.HasOne(d => d.MgrempnoNavigation).WithOne(p => p.Department)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_departments_employees_mgrempno");
        });

        modelBuilder.Entity<Empdependent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empdependents_pkey");

            entity.HasOne(d => d.EmpnoNavigation).WithMany(p => p.Empdependents)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_empdependents_employees_empno");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Empno).HasName("employees_pkey");

            entity.Property(e => e.Status).HasDefaultValueSql("'Active'::character varying");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_departments_deptno");

            entity.HasOne(d => d.SupervisorempnoNavigation).WithMany(p => p.InverseSupervisorempnoNavigation)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_employees_supervisonempno");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Locationid).HasName("locations_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Locations)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_locations_departments_deptno");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Projno).HasName("projects_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_projects_departments_deptno");
        });

        modelBuilder.Entity<Workson>(entity =>
        {
            entity.HasKey(e => new { e.Empno, e.Projno }).HasName("worksons_pkey");

            entity.Property(e => e.Dateworked).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.Hoursworked).HasDefaultValue(0);

            entity.HasOne(d => d.EmpnoNavigation).WithMany(p => p.Worksons)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_worksons_employees_empno");

            entity.HasOne(d => d.ProjnoNavigation).WithMany(p => p.Worksons)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_worksons_projects_projno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
