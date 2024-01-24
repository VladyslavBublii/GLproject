using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Models
{
	public class ProductViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter the name of the product")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be from 3 to 255 characters long")]
		public string Name { get; set; }

		[StringLength(50, MinimumLength = 3, ErrorMessage = "The category must be from 3 to 255 characters long")]
		public string Category { get; set; }

		[DataType(DataType.MultilineText)]
		[StringLength(500, MinimumLength = 3, ErrorMessage = "The description must be from 3 to 1000 characters long")]
		public string Description { get; set; }

		[Display(Name = "Price, $")]
		[Required(ErrorMessage = "Please enter the price of the product")]
		[Range(0.01, 999_999.99, ErrorMessage = "Please enter a positive value of the price")]
		[DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
		public decimal Price { get; set; }

		public IFormFile ImageIn { get; set; }

		public string ImageName { get; set; }

	}
}
