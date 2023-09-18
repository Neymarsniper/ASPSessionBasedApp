using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SeesionASPCore.Models;

public partial class DbFirstApprochContext : DbContext
{
    public DbFirstApprochContext()
    {
    }

    public DbFirstApprochContext(DbContextOptions<DbFirstApprochContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<UserCred> UserCreds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Employee>(entity =>
        //{
        //    entity.HasKey(e => e.EmpId);

        //    entity.ToTable("Employee");

        //    entity.Property(e => e.EmpId)
        //        .ValueGeneratedNever()
        //        .HasColumnName("Emp_Id");
        //    entity.Property(e => e.City)
        //        .HasMaxLength(20)
        //        .IsUnicode(false);
        //    entity.Property(e => e.EmpName)
        //        .HasMaxLength(20)
        //        .IsUnicode(false)
        //        .HasColumnName("Emp_Name");
        //    entity.Property(e => e.Grade)
        //        .HasMaxLength(1)
        //        .IsUnicode(false);
        //});

        modelBuilder.Entity<UserCred>(entity =>
        {
            entity.ToTable("UserCred");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
