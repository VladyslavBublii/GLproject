using System;

namespace Core.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string City { get; set; }

        public string PostIndex { get; set; }

        public Guid UserId { set; get; }

        public User User { set; get; }
    }
}
