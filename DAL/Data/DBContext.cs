using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }

        //change for you server name
        private readonly string computerName = "Legion-5-Pro";
        //private readonly string computerName = "DESKTOP-GM1CRMU";

        public DBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server={computerName}; Database=Database4; Trusted_Connection=True; TrustServerCertificate=True;");
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
