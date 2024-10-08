﻿// <auto-generated />
using System;
using HRIS.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRIS.Persistance.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240905092754_ChangeStartDateAndEndDateToDateOnly")]
    partial class ChangeStartDateAndEndDateToDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HRIS.Domain.Entity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Department", b =>
                {
                    b.Property<int>("Deptno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Deptno"));

                    b.Property<string>("Deptname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("deptname");

                    b.Property<string>("Mgrempno")
                        .HasColumnType("text")
                        .HasColumnName("mgrempno");

                    b.HasKey("Deptno");

                    b.HasIndex(new[] { "Mgrempno" }, "departments_mgrempno_key")
                        .IsUnique();

                    b.ToTable("departments");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.EmpDependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("Empno")
                        .HasColumnType("text")
                        .HasColumnName("empno");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("relationship");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("sex");

                    b.HasKey("Id");

                    b.HasIndex("Empno");

                    b.ToTable("EmpDependents");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("DeactivatedAt")
                        .HasColumnType("date")
                        .HasColumnName("deactivated_at");

                    b.Property<string>("Deactreason")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("deactreason");

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Employmenttype")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("employmenttype");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("fname");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("lname");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric")
                        .HasColumnName("salary");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("sex");

                    b.Property<string>("Ssn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("ssn");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("status");

                    b.Property<string>("Supervisorempno")
                        .HasColumnType("text")
                        .HasColumnName("supervisorempno");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("Deptno");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("Supervisorempno");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("locationid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.HasKey("Id");

                    b.HasIndex("Deptno");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Project", b =>
                {
                    b.Property<int>("Projno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Projno"));

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<string>("Projname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("projname");

                    b.HasKey("Projno");

                    b.HasIndex("Deptno");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.LeaveRequest", b =>
                {
                    b.Property<int>("LeaveRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LeaveRequestId"));

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("LeaveRequestId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProcessId")
                        .IsUnique();

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.NextStepRule", b =>
                {
                    b.Property<int>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RuleId"));

                    b.Property<string>("ConditionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConditionValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CurrentStepId")
                        .HasColumnType("integer");

                    b.Property<int>("NextStepId")
                        .HasColumnType("integer");

                    b.HasKey("RuleId");

                    b.HasIndex("CurrentStepId");

                    b.HasIndex("NextStepId");

                    b.ToTable("NextStepRules");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.Process", b =>
                {
                    b.Property<int>("ProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProcessId"));

                    b.Property<int>("CurrentStepId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RequestType")
                        .HasColumnType("text");

                    b.Property<string>("RequesterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer");

                    b.HasKey("ProcessId");

                    b.HasIndex("CurrentStepId");

                    b.HasIndex("RequesterId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.Workflow", b =>
                {
                    b.Property<int>("WorkflowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WorkflowId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("WorkflowName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WorkflowId");

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.WorkflowAction", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ActionId"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ActorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<int>("StepId")
                        .HasColumnType("integer");

                    b.HasKey("ActionId");

                    b.HasIndex("ActorId");

                    b.HasIndex("ProcessId");

                    b.HasIndex("StepId");

                    b.ToTable("WorkflowActions");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.WorkflowSequence", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StepId"));

                    b.Property<string>("RequiredRoleId")
                        .HasColumnType("text");

                    b.Property<string>("StepName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("StepOrder")
                        .HasColumnType("integer");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer");

                    b.HasKey("StepId");

                    b.HasIndex("RequiredRoleId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("WorkflowSequences");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workson", b =>
                {
                    b.Property<string>("Empno")
                        .HasColumnType("text")
                        .HasColumnName("empno");

                    b.Property<int>("Projno")
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    b.Property<DateOnly?>("Dateworked")
                        .HasColumnType("date")
                        .HasColumnName("dateworked");

                    b.Property<int?>("Hoursworked")
                        .HasColumnType("integer")
                        .HasColumnName("hoursworked");

                    b.HasKey("Empno", "Projno");

                    b.HasIndex("Projno");

                    b.ToTable("worksons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Department", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", "MgrempnoNavigation")
                        .WithOne("Department")
                        .HasForeignKey("HRIS.Domain.Entity.Department", "Mgrempno");

                    b.Navigation("MgrempnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.EmpDependent", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", "EmpnoNavigation")
                        .WithMany("Empdependents")
                        .HasForeignKey("Empno");

                    b.Navigation("EmpnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Employee", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Department", "DeptnoNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Deptno");

                    b.HasOne("HRIS.Domain.Entity.Employee", "SupervisorempnoNavigation")
                        .WithMany("InverseSupervisorempnoNavigation")
                        .HasForeignKey("Supervisorempno");

                    b.Navigation("DeptnoNavigation");

                    b.Navigation("SupervisorempnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Location", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Department", "DeptnoNavigation")
                        .WithMany("Locations")
                        .HasForeignKey("Deptno");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Project", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Department", "DeptnoNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("Deptno");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.LeaveRequest", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", "EmployeeIdNavigation")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Workflow.Process", "ProcessIdNavigation")
                        .WithOne("LeaveRequestNavigation")
                        .HasForeignKey("HRIS.Domain.Entity.Workflow.LeaveRequest", "ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeIdNavigation");

                    b.Navigation("ProcessIdNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.NextStepRule", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Workflow.WorkflowSequence", "CurrentStepIdNavigation")
                        .WithMany("CurrentStepIds")
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Workflow.WorkflowSequence", "NextStepIdNavigation")
                        .WithMany("NextStepIds")
                        .HasForeignKey("NextStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentStepIdNavigation");

                    b.Navigation("NextStepIdNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.Process", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Workflow.WorkflowSequence", "CurrentStepIdNavigation")
                        .WithMany("Processes")
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Employee", "RequesterIdNavigation")
                        .WithMany()
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Workflow.Workflow", "WorkflowIdNavigation")
                        .WithMany("Processes")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentStepIdNavigation");

                    b.Navigation("RequesterIdNavigation");

                    b.Navigation("WorkflowIdNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.WorkflowAction", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", "ActorIdNavigation")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Workflow.Process", "ProcessIdNavigation")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Workflow.WorkflowSequence", "StepIdNavigation")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorIdNavigation");

                    b.Navigation("ProcessIdNavigation");

                    b.Navigation("StepIdNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.WorkflowSequence", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.AppRole", "RequiredRoleIdNavigation")
                        .WithMany()
                        .HasForeignKey("RequiredRoleId");

                    b.HasOne("HRIS.Domain.Entity.Workflow.Workflow", "Workflow")
                        .WithMany("WorkflowSequences")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequiredRoleIdNavigation");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workson", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", "EmpnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Empno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Project", "ProjnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Projno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmpnoNavigation");

                    b.Navigation("ProjnoNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entity.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entity.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Locations");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Employee", b =>
                {
                    b.Navigation("Department");

                    b.Navigation("Empdependents");

                    b.Navigation("InverseSupervisorempnoNavigation");

                    b.Navigation("LeaveRequests");

                    b.Navigation("Worksons");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Project", b =>
                {
                    b.Navigation("Worksons");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.Process", b =>
                {
                    b.Navigation("LeaveRequestNavigation");

                    b.Navigation("WorkflowActions");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.Workflow", b =>
                {
                    b.Navigation("Processes");

                    b.Navigation("WorkflowSequences");
                });

            modelBuilder.Entity("HRIS.Domain.Entity.Workflow.WorkflowSequence", b =>
                {
                    b.Navigation("CurrentStepIds");

                    b.Navigation("NextStepIds");

                    b.Navigation("Processes");

                    b.Navigation("WorkflowActions");
                });
#pragma warning restore 612, 618
        }
    }
}
