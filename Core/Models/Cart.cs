using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> ProductsId { get; set; }
        //public int Amount { get; set; }
        public decimal Sum { get; set; }
    }
}
