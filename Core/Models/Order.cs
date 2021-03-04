using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class Order // Отсутствует статус заказа, кол-во товара
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Sum { get; set; }
        //public string Name { get; set; } // Пока без привязки к Customer(UsersDetails) - *********
        public string Surname { get; set; } // Пока без привязки к Customer(UsersDetails)
        public string PhoneNumber { get; set; } // Свойство отсутствующее в свойствах Customer(UsersDetails) --- значит надо было его добавить !
        public string City { get; set; } // Пока без привязки к Customer(UsersDetails)
        public string PostIndex { get; set; } // Пока без привязки к Customer(UsersDetails)
        public Guid ProductId { get; set; }
        public List<Product> Products { get; set; }

        public DateTime Date { get; set; }
    }
}
