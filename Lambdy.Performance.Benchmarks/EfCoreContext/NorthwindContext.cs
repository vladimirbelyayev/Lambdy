﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetailsV> ProductDetailsVs { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=DataSource/Northwind_large.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Description).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ContactName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ContactTitle).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Fax).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.ToTable("CustomerCustomerDemo");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.CustomerTypeId).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<CustomerDemographic>(entity =>
            {
                entity.ToTable("CustomerDemographic");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.CustomerDesc).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.BirthDate).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Extension).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.FirstName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.HireDate).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.HomePhone).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.LastName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Notes).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.PhotoPath).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Title).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.TitleOfCourtesy).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<EmployeeTerritory>(entity =>
            {
                entity.ToTable("EmployeeTerritory");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.TerritoryId).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Freight)
                    .IsRequired()
                    .HasColumnType("DECIMAL");

                entity.Property(e => e.OrderDate).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.RequiredDate).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipAddress).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipCity).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipCountry).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipPostalCode).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShipRegion).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ShippedDate).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Discount).HasColumnType("DOUBLE");

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("DECIMAL");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.QuantityPerUnit).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("DECIMAL");
            });

            modelBuilder.Entity<ProductDetailsV>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProductDetails_V");

                entity.Property(e => e.CategoryDescription).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.CategoryName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ProductName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.QuantityPerUnit).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.SupplierName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.SupplierRegion).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.UnitPrice).HasColumnType("DECIMAL");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RegionDescription).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ContactName).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.ContactTitle).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Fax).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.HomePage).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.ToTable("Territory");

                entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");

                entity.Property(e => e.TerritoryDescription).HasColumnType("VARCHAR(8000)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
