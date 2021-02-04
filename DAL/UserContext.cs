using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class UserContext : DbContext
    {
        private readonly string _conStr;

        public DbSet<User> Users { get; set; }

        public UserContext()
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
        }
    }
}
