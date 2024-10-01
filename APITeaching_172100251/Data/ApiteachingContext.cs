using System;
using System.Collections.Generic;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Data;

public partial class ApiteachingContext : DbContext
{
    public ApiteachingContext()
    {
    }

    public ApiteachingContext(DbContextOptions<ApiteachingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=APITeaching; Persist Security Info=True; User ID=sa; Password=123456; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Addresses");

            entity.ToTable("Address");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddressText)
                .HasMaxLength(100)
                .HasColumnName("address_text");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TownId).HasColumnName("town_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Address_Country");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Address_District");

            entity.HasOne(d => d.Province).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Address_Province");

            entity.HasOne(d => d.Ward).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.WardId)
                .HasConstraintName("FK_Address_Ward");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "CategoryName");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ParentId)
                .HasDefaultValue(0)
                .HasColumnName("parent_id");
            entity.Property(e => e.Picture).HasColumnType("image");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(5)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Customers_Account");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_District");

            entity.ToTable("District");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(5)
                .HasColumnName("district_code");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.LastName, "LastName");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(10);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.PhotoPath).HasMaxLength(255);
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.Account).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Employees_Account");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "CustomerID");

            entity.HasIndex(e => e.CustomerId, "CustomersOrders");

            entity.HasIndex(e => e.EmployeeId, "EmployeeID");

            entity.HasIndex(e => e.EmployeeId, "EmployeesOrders");

            entity.HasIndex(e => e.OrderDate, "OrderDate");

            entity.HasIndex(e => e.ShippedDate, "ShippedDate");

            entity.HasIndex(e => e.ShipId, "ShippersOrders");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Freight)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.RequiredDate).HasColumnType("datetime");
            entity.Property(e => e.ShipAddress).HasMaxLength(60);
            entity.Property(e => e.ShipId).HasColumnName("Ship_id");
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.Ship).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_Orders_Shippers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK_Order_Details");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.HasIndex(e => e.OrderId, "OrdersOrder_Details");

            entity.HasIndex(e => e.ProductId, "ProductID");

            entity.HasIndex(e => e.ProductId, "ProductsOrder_Details");

            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Quantity).HasDefaultValue((short)1);
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

            entity.HasIndex(e => e.CategoryId, "CategoryID");

            entity.HasIndex(e => e.ProductName, "ProductName");

            entity.HasIndex(e => e.SupplierId, "SupplierID");

            entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.ReorderLevel).HasDefaultValue((short)0);
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.UnitsInStock).HasDefaultValue((short)0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue((short)0);
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Province");

            entity.ToTable("Province");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AxisMeridian)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("axis_meridian");
            entity.Property(e => e.CountryId)
                .HasDefaultValue(1)
                .HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(5)
                .HasColumnName("province_code");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.ToTable("RoleUser");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Account).WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleUser_Account");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleUser_Roles");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HomePage).HasColumnType("ntext");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Wards");

            entity.ToTable("Ward");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WardCode)
                .HasMaxLength(5)
                .HasColumnName("ward_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
