using System;
using System.Collections.Generic;

namespace BL.DTO
{
    public class CartDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public decimal Sum { get; set; }
    }
}
