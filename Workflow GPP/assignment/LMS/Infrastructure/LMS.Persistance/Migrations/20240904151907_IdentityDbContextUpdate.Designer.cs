﻿// <auto-generated />
using System;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMS.Persistance.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240904151907_IdentityDbContextUpdate")]
    partial class IdentityDbContextUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LMS.Domain.Entities.AppRole", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

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

                    b.Property<decimal?>("Penalty")
                        .HasColumnType("numeric");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("LMS.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateCreated")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateDeleted")
                        .HasColumnType("date");

                    b.Property<string>("DeleteReason")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PurchaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Lending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("BorrowDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateCreated")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateDeleted")
                        .HasColumnType("date");

                    b.Property<string>("DeleteReason")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DueReturnDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("BookId");

                    b.ToTable("Lendings");
                });

            modelBuilder.Entity("LMS.Domain.Entities.LibraryCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateCreated")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateDeleted")
                        .HasColumnType("date");

                    b.Property<string>("DeleteReason")
                        .HasColumnType("text");

                    b.Property<DateOnly>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("LibraryCards");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.BookRequest", b =>
                {
                    b.Property<int>("BookRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookRequestId"));

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .HasColumnType("text");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("BookRequestId");

                    b.HasIndex("ProcessId")
                        .IsUnique();

                    b.ToTable("BookRequests");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.NextStepRule", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.Process", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.Workflow", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.WorkflowAction", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.WorkflowSequence", b =>
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

            modelBuilder.Entity("LMS.Domain.Entities.Lending", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", "AppUser")
                        .WithMany("Lendings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.Book", "Book")
                        .WithMany("Lendings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LMS.Domain.Entities.LibraryCard", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", "AppUser")
                        .WithOne("LibraryCard")
                        .HasForeignKey("LMS.Domain.Entities.LibraryCard", "AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.BookRequest", b =>
                {
                    b.HasOne("LMS.Domain.Entities.Workflow.Process", "ProcessIdNavigation")
                        .WithOne("BookRequestNavigation")
                        .HasForeignKey("LMS.Domain.Entities.Workflow.BookRequest", "ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessIdNavigation");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.NextStepRule", b =>
                {
                    b.HasOne("LMS.Domain.Entities.Workflow.WorkflowSequence", "CurrentStepIdNavigation")
                        .WithMany("CurrentStepIds")
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.Workflow.WorkflowSequence", "NextStepIdNavigation")
                        .WithMany("NextStepIds")
                        .HasForeignKey("NextStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentStepIdNavigation");

                    b.Navigation("NextStepIdNavigation");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.Process", b =>
                {
                    b.HasOne("LMS.Domain.Entities.Workflow.WorkflowSequence", "CurrentStepIdNavigation")
                        .WithMany("Processes")
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.AppUser", "RequesterIdNavigation")
                        .WithMany("Processes")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.Workflow.Workflow", "WorkflowIdNavigation")
                        .WithMany("Processes")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentStepIdNavigation");

                    b.Navigation("RequesterIdNavigation");

                    b.Navigation("WorkflowIdNavigation");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.WorkflowAction", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", "ActorIdNavigation")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.Workflow.Process", "ProcessIdNavigation")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.Workflow.WorkflowSequence", "StepIdNavigation")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorIdNavigation");

                    b.Navigation("ProcessIdNavigation");

                    b.Navigation("StepIdNavigation");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.WorkflowSequence", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppRole", "RequiredRoleIdNavigation")
                        .WithMany("WorkflowSequences")
                        .HasForeignKey("RequiredRoleId");

                    b.HasOne("LMS.Domain.Entities.Workflow.Workflow", "Workflow")
                        .WithMany("WorkflowSequences")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequiredRoleIdNavigation");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LMS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LMS.Domain.Entities.AppRole", b =>
                {
                    b.Navigation("WorkflowSequences");
                });

            modelBuilder.Entity("LMS.Domain.Entities.AppUser", b =>
                {
                    b.Navigation("Lendings");

                    b.Navigation("LibraryCard");

                    b.Navigation("Processes");

                    b.Navigation("WorkflowActions");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Book", b =>
                {
                    b.Navigation("Lendings");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.Process", b =>
                {
                    b.Navigation("BookRequestNavigation");

                    b.Navigation("WorkflowActions");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.Workflow", b =>
                {
                    b.Navigation("Processes");

                    b.Navigation("WorkflowSequences");
                });

            modelBuilder.Entity("LMS.Domain.Entities.Workflow.WorkflowSequence", b =>
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
