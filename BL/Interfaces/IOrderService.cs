using BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        ProductDTO GetPhone(int? id);
        IEnumerable<ProductDTO> GetPhones();
        void Dispose();
    }
}
