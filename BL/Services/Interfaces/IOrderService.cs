﻿using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	public interface IOrderService
	{
		void MakeOrder(OrderDTO orderDto);
		ProductDTO GetProduct(int? id);
		IEnumerable<ProductDTO> GetProducts();
		//void Dispose();
	}
}
