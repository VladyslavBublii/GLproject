﻿using Azure;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Data
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        private readonly string computerName = Environment.MachineName;

        public DBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server={computerName}; Database=StoreDB; Trusted_Connection=True; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(a => a.Customer)
                .WithOne(b => b.User)
                .HasForeignKey<Customer>(b => b.UserId);
            
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrdersId, op.ProductsId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrdersId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductsId);
        }
    }
}
