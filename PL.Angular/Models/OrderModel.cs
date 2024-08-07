using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Angular.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public decimal Sum { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostIndex { get; set; }

        public Guid UserId { get; set; }
        public ICollection<Guid> ProductIds { get; set; }
    }
}
