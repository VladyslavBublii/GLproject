using System;

namespace Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductsId { get; set; }
    }
}
