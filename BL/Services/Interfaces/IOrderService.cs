using System;
using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	public interface IOrderService
	{
		void MakeOrder(OrderDTO orderDto);

		ProductDTO GetProduct(Guid id);

		IEnumerable<ProductDTO> GetProducts();

		IEnumerable<OrderDTO> GetOrdersByUserId(Guid userId);
    }
}
