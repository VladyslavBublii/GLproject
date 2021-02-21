using System.Collections.Generic;

namespace BL.DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string Category { get; set; } // Пока вместо отдельной таблицы
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		// Пока без таблицы с изображениями
	}
}
