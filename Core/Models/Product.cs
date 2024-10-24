﻿using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class Product
	{
		public Guid Id { get; set; }

		public string Category { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public string ImageName { get; set; }

		public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
