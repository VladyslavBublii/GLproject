namespace PL.Angular.Models
{
    public class OrderedProduct
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Category { get; set; }

        public required decimal Price { get; set; }

        public required uint Count { get; set; }

        public required string ImageName { get; set; }

        public required string UrlImage { get; set; }
    }
}
