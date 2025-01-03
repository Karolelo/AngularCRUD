using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.DbContext;

public partial class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Shop;User=sa;Password=Haslo1234*;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pk");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Category_name");

            entity.HasMany(d => d.Products).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_category_Product"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Product_category_Category"),
                    j =>
                    {
                        j.HasKey("CategoryId", "ProductId").HasName("Product_category_pk");
                        j.ToTable("Product_category");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("Category_ID");
                        j.IndexerProperty<int>("ProductId").HasColumnName("Product_ID");
                    });
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Magazine_pk");

            entity.ToTable("Magazine");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithOne(p => p.Magazine)
                .HasForeignKey<Magazine>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Magazyn_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pk");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImgSourcePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("img_source_path");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_data_pk");

            entity.ToTable("User_data");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_ID");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_date");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e=>e.Email).HasMaxLength(50).IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.UserDatum)
                .HasForeignKey<UserDatum>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_data_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
