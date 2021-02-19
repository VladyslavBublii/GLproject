using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
	public class Product
	{
		public int Id { get; set; }
		public int Category { get; set; } // Пока вместо отдельной таблицы
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public ICollection<Order> Orders { get; set; } // Вместо OrdersContents
		
		// Пока без таблицы с изображениями
	}
}
