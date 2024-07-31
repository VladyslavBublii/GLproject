using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class OrderProduct
    {
        public Guid OrdersId { get; set; }

        public Order Order { get; set; }

        public Guid ProductsId { get; set; }

        public Product Product { get; set; }

        public int NumberOfProduct { get; set; }
    }
}
