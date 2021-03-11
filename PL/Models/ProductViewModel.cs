using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PL.Models
{
	public class ProductViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter the name of the product")]
		[StringLength(255, MinimumLength = 3, ErrorMessage = "The name must be from 3 to 255 characters long")]
		public string Name { get; set; }

		public string Category { get; set; } // Пока вместо отдельной таблицы

		[DataType(DataType.MultilineText)]
		[StringLength(1000, MinimumLength = 3, ErrorMessage = "The description must be from 3 to 1000 characters long")]
		public string Description { get; set; }

		[Display(Name = "Price, $")]
		//[Required(ErrorMessage = "Please enter the description of the product")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive value of the price")]
		public decimal Price { get; set; }
		public IFormFile ImageIn { get; set; }
		public byte[] Image { get; set; }


	}
}
