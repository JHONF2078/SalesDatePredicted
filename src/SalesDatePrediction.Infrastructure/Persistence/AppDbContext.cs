using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Infrastructure.Persistence
{
    public  class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Product> Products { get; set; }       
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      

            modelBuilder.Entity<Employee>()
                .ToTable("Employees", "HR")
                .HasKey(e => e.EmpId);


            modelBuilder.Entity<Shipper>()
                .ToTable("Shippers", "Sales")
                .HasKey(s => s.ShipperId);

            modelBuilder.Entity<Product>()
                .ToTable("Products", "Production")
                .HasKey(p => p.ProductId);


            modelBuilder.Entity<Order>()
                .ToTable("Orders", "Sales")
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .ToTable("OrderDetails", "Sales")
                .HasKey(od => new { od.OrderId, od.ProductId });


            modelBuilder.Entity<Supplier>()
                .ToTable("Suppliers", "Production")
                .HasKey(s => s.SupplierId);

            modelBuilder.Entity<Category>()
                .ToTable("Categories", "Production")
                .HasKey(c => c.CategoryId);


            modelBuilder.Entity<Customer>()
               .ToTable("Customers", "Sales")
                .HasKey(c => c.CustId);

            // Configuración de la relación Order - Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustId)
                .HasPrincipalKey(c => c.CustId)
                .HasConstraintName("FK_Orders_Customers");

            // Configuración de la relación Order - Employee
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmpId)
                .HasPrincipalKey(e => e.EmpId)
                .HasConstraintName("FK_Orders_Employees");


            // Configuración adicional para evitar bucles en las serializaciones JSON
            //modelBuilder.Entity<Order>()
            //    .Ignore(o => o.OrderDetails);

            //modelBuilder.Entity<Customer>()
            //    .Ignore(c => c.Orders);

            //modelBuilder.Entity<Employee>()
            //    .Ignore(e => e.Orders);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
