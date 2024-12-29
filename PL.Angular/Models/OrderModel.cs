using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Angular.Models
{
    public class OrderModel
    {
        public required Guid Id { get; set; }

        public required decimal Sum { get; set; }

        public required string PhoneNumber { get; set; }

        public required string City { get; set; }

        public required string PostIndex { get; set; }

        public required Guid UserId { get; set; }

        public required ICollection<Guid> ProductIds { get; set; }

        public required ICollection<OrderedProduct> OrderedProducts { get; set; }
    }
}
