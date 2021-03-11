using System;


namespace PL.Models
{
	public class ProductViewModel
	{
		public Guid Id { get; set; }
		public string Category { get; set; } // Пока вместо отдельной таблицы
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
