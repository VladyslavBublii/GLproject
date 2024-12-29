using System.ComponentModel.DataAnnotations;

namespace PL.Angular.Models
{
    public class MainProductInformation
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        
        public required string ImageName { get; set; }

        public required string UrlImage { get; set; }
    }
}
