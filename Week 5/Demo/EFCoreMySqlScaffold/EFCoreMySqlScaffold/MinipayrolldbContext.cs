using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EFCoreMySqlScaffold;

public partial class MinipayrolldbContext : DbContext
{
    public MinipayrolldbContext()
    {
    }

    public MinipayrolldbContext(DbContextOptions<MinipayrolldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Prla01> Prla01s { get; set; }

    public virtual DbSet<Prlm01> Prlm01s { get; set; }

    public virtual DbSet<Prlm02> Prlm02s { get; set; }

    public virtual DbSet<Prlt01> Prlt01s { get; set; }

    public virtual DbSet<VwEmployeeSummary> VwEmployeeSummaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=nisharg2004;database=minipayrolldb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.43-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Prla01>(entity =>
        {
            entity.HasKey(e => e.T04f01).HasName("PRIMARY");

            entity.ToTable("prla01");

            entity.Property(e => e.T04f01).HasColumnName("t04f01");
            entity.Property(e => e.T04f02).HasColumnName("t04f02");
            entity.Property(e => e.T04f03)
                .HasPrecision(10, 2)
                .HasColumnName("t04f03");
            entity.Property(e => e.T04f04)
                .HasPrecision(10, 2)
                .HasColumnName("t04f04");
            entity.Property(e => e.T04f05)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("t04f05");
            entity.Property(e => e.T04f06)
                .HasMaxLength(150)
                .HasColumnName("t04f06");
        });

        modelBuilder.Entity<Prlm01>(entity =>
        {
            entity.HasKey(e => e.T01f01).HasName("PRIMARY");

            entity.ToTable("prlm01");

            entity.HasIndex(e => e.T01f02, "t01f02").IsUnique();

            entity.Property(e => e.T01f01).HasColumnName("t01f01");
            entity.Property(e => e.T01f02)
                .HasMaxLength(100)
                .HasColumnName("t01f02");
        });

        modelBuilder.Entity<Prlm02>(entity =>
        {
            entity.HasKey(e => e.T02f01).HasName("PRIMARY");

            entity.ToTable("prlm02");

            entity.HasIndex(e => e.T02f02, "idx_emp_name");

            entity.HasIndex(e => e.T02f03, "t02f03");

            entity.Property(e => e.T02f01).HasColumnName("t02f01");
            entity.Property(e => e.T02f02)
                .HasMaxLength(150)
                .HasColumnName("t02f02");
            entity.Property(e => e.T02f03).HasColumnName("t02f03");
            entity.Property(e => e.T02f04)
                .HasPrecision(10, 2)
                .HasColumnName("t02f04");

            entity.HasOne(d => d.T02f03Navigation).WithMany(p => p.Prlm02s)
                .HasForeignKey(d => d.T02f03)
                .HasConstraintName("prlm02_ibfk_1");
        });

        modelBuilder.Entity<Prlt01>(entity =>
        {
            entity.HasKey(e => e.T03f01).HasName("PRIMARY");

            entity.ToTable("prlt01");

            entity.HasIndex(e => e.T03f02, "t03f02");

            entity.Property(e => e.T03f01).HasColumnName("t03f01");
            entity.Property(e => e.T03f02).HasColumnName("t03f02");
            entity.Property(e => e.T03f03)
                .HasMaxLength(20)
                .HasColumnName("t03f03");
            entity.Property(e => e.T03f04)
                .HasPrecision(10, 2)
                .HasColumnName("t03f04");
            entity.Property(e => e.T03f05).HasColumnName("t03f05");

            entity.HasOne(d => d.T03f02Navigation).WithMany(p => p.Prlt01s)
                .HasForeignKey(d => d.T03f02)
                .HasConstraintName("prlt01_ibfk_1");
        });

        modelBuilder.Entity<VwEmployeeSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_employee_summary");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("department_name");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(150)
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeeSalary)
                .HasPrecision(10, 2)
                .HasColumnName("employee_salary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
