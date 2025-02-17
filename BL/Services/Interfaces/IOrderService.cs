using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces;

public interface IOrderService
{
    Task MakeOrderAsync(OrderDTO orderDto);

    Task<ProductDTO> GetProductAsync(Guid id);

    Task<IEnumerable<ProductDTO>> GetProductsAsync();

    Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(Guid userId);
}