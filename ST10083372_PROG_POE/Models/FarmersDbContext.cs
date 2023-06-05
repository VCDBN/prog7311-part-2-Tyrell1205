using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ST10083372_PROG_POE.Models;

public partial class FarmersDbContext : DbContext
{
    public FarmersDbContext()
    {
    }

    public FarmersDbContext(DbContextOptions<FarmersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<FarmerProduct> FarmerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmersDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF15298E3E8");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PK__Farmers__731B88E8DD4CD094");

            entity.Property(e => e.FarmerId).HasColumnName("FarmerID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FarmerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            
        });

        modelBuilder.Entity<FarmerProduct>(entity =>
        {
            entity.HasKey(e => e.FarmerProductId).HasName("PK__FarmerPr__CD3950D19AC977C4");

            entity.Property(e => e.FarmerProductId).HasColumnName("FarmerProductID");
            entity.Property(e => e.FarmerId).HasColumnName("FarmerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Farmer).WithMany(p => p.FarmerProducts)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FarmerPro__Farme__4F7CD00D");

            entity.HasOne(d => d.Product).WithMany(p => p.FarmerProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FarmerPro__Produ__5070F446");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED4AEB7969");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplyDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
