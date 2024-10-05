namespace PL.Angular.Models
{
    public class OrderedProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public uint Count { get; set; }

        public string ImageName { get; set; }

        public string UrlImage { get; set; }
    }
}
