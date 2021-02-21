using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	interface IOrderService
	{
		void MakeOrder(OrderDTO orderDto);
		ProductDTO GetProduct(int? id);
		IEnumerable<ProductDTO> GetProducts();
		void Dispose();
	}
}
