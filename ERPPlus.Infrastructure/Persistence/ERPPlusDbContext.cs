using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPPlus.Infrastructure.Persistence
{
    public class ERPPlusDbContext : DbContext
    {
        public ERPPlusDbContext(DbContextOptions<ERPPlusDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductOrder>().HasKey(po => new {po.OrderId, po.ProductId});

            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Order).WithMany(o => o.ProductOrders).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Product).WithMany(o => o.ProductOrders).HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductOrder>().ToTable("ProductOrders");


        }
    }
}
