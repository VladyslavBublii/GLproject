using Core.Enums;
using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class Order 
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; } 

        public string PostIndex { get; set; }

        public decimal Sum { get; set; }

        public DateTime OrderTime { get; set; }
    }
}
