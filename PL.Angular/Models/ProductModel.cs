namespace PL.Angular.Models
{
    public class ProductModel
    {
        public required string Category { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required decimal Price { get; set; }

        public required string ImageName { get; set; }
    }
}
