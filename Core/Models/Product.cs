using System;

namespace Core.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Category { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public Guid OrderId {get; set;}
	}
}
