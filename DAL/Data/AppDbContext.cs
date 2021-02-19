using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.Entity;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        static AppDbContext()
        {
			Database.SetInitializer<AppDbContext>(new StoreDbInitializer());
        }
        public AppDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext db)
        {
            db.Products.Add(new Product { Name = "Nokia Lumia 630", Description = "Nokia", Price = 220 });
            db.Products.Add(new Product { Name = "iProduct 6", Description = "Apple", Price = 320 });
            db.Products.Add(new Product { Name = "LG G4", Description = "lG", Price = 260 });
            db.Products.Add(new Product { Name = "Samsung Galaxy S 6", Description = "Samsung", Price = 300 });
            db.SaveChanges();
        }
    }
}
