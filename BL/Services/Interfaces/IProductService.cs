using System;
using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	public interface IProductService
	{
		IEnumerable<ProductDTO> GetProducts();
		ProductDTO GetProduct(Guid id);
		void Create(ProductDTO productDTO);
		void Update(ProductDTO productDTO);
		ProductDTO Find(Guid id);
		ProductDTO Delete(Guid id);
	}
}
