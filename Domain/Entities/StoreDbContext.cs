using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoreApi.Domain.Model;

namespace StoreApi.Domain.Entities;

public partial class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
    }

    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-868J7S1\\SQLEXPRESS;Initial Catalog=StoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Customer>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK_User");

        //    entity.ToTable("Customer");

        //    entity.Property(e => e.Id).HasColumnName("ID");
        //    entity.Property(e => e.Address).HasMaxLength(50);
        //    entity.Property(e => e.Email).HasMaxLength(100);
        //    entity.Property(e => e.Name).HasMaxLength(50);
        //    entity.Property(e => e.Password).HasMaxLength(8);
        //    entity.Property(e => e.RefreshToken).HasMaxLength(500);
        //    entity.Property(e => e.RefreshTokenEndDate).HasColumnType("datetime");
        //    entity.Property(e => e.Surname).HasMaxLength(50);
        //});

        //modelBuilder.Entity<Product>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK_Product");

        //    entity.Property(e => e.Id).HasColumnName("ID");
        //    entity.Property(e => e.Name).HasMaxLength(50);
        //    entity.Property(e => e.Price).HasColumnType("money");
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
