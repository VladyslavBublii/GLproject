using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductsId { get; set; }
        //public int Amount { get; set; }
        public decimal Sum { get; set; }
    }
}
