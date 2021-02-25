using System;
using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	interface IOrderService
	{
		void MakeOrder(OrderDTO orderDto);
		ProductDTO GetProduct(Guid id);
		IEnumerable<ProductDTO> GetProducts();
		void Dispose();
	}
}
