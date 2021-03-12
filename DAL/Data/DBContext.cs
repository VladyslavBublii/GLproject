using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.Data
{
    public class DBContext : DbContext
    {
        private readonly string _conStr;

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Cart { get; set; }
        //public DbSet<Cart> Cart { get; set; }

        public DBContext()
        {
            var builder = new ConfigurationBuilder();
            string fileName = @"pathdatabase\DatabaseConnection.json";
            builder.AddJsonFile(Path.GetFullPath(fileName));
            IConfigurationRoot config = builder.Build();
            _conStr = config["ConnectionString"];
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(a => a.Customer)
                .WithOne(b => b.User)
                .HasForeignKey<Customer>(b => b.UserId);
        }
    }
}
