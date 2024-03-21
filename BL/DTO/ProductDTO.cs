using System;
using System.ComponentModel.DataAnnotations;


namespace BL.DTO
{
	public class ProductDTO
	{
		public Guid Id { get; set; }

		public string Category { get; set; } 

		public string Name { get; set; }

		public string Description { get; set; }

		[DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
		public decimal Price { get; set; }

		public string ImageName { get; set; }
	}
}
