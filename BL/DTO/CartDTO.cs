using System;

namespace BL.DTO
{
    class CartDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductsId { get; set; }

        public double Sum { get; set; }
    }
}
