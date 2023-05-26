using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class Product
	{
		public Product()
		{
			Orders = new HashSet<Order>();
		}

		public Guid Id { get; set; }
		public string Category { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
