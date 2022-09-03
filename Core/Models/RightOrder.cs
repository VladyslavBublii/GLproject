using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class RightOrder
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
