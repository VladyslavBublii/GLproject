using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
	public class ProductDTO
	{
		public Guid Id { get; set; }
		public string Category { get; set; } // Пока вместо отдельной таблицы

		//[Required(ErrorMessage = "Please enter the name of the product")]
		public string Name { get; set; }

		//[Required(ErrorMessage = "Please enter the description of the product")]
		//[Required]
		public string Description { get; set; }

		//[Required]
		//[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive value for the price")]
		public decimal Price { get; set; }

		public byte[] Image { get; set; }

		// Пока без таблицы с изображениями
	}
}
