using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();

    Task<ProductDTO> GetProductAsync(Guid id);

    Task CreateAsync(ProductDTO productDTO);

    Task UpdateAsync(ProductDTO productDTO);

    Task<ProductDTO> FindAsync(Guid id);

    Task<ProductDTO> DeleteAsync(Guid id);

    Task<bool> CheckItemAsync(Guid idItem);
}