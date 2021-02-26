using System.Collections.Generic;
using BL.DTO;

namespace BL.Services.Interfaces
{
	public interface IProductService
	{
		void Create(ProductDTO productDTO);
		ProductDTO GetProduct(int? id);
		IEnumerable<ProductDTO> GetProducts();
		//void Dispose();
	}
}
