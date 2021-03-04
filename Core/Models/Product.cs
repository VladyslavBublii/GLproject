﻿using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Category { get; set; } // Пока вместо отдельной таблицы
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		//public ICollection<Order> Orders { get; set; } // Вместо OrdersContents

		// Пока без таблицы с изображениями
	}
}
