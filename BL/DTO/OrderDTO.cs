using System;

namespace BL.DTO
{
	public class OrderDTO // Отсутствует статус заказа, кол-во товара
    {
        public Guid Id { get; set; }
        public decimal Sum { get; set; }
        public string Name { get; set; } // Пока без привязки к Customer(UsersDetails)
        public string Surname { get; set; } // Пока без привязки к Customer(UsersDetails)
        public string PhoneNumber { get; set; } // Свойство отсутствующее в свойствах Customer(UsersDetails)
        public string City { get; set; } // Пока без привязки к Customer(UsersDetails)
        public string PostIndex { get; set; } // Пока без привязки к Customer(UsersDetails)

        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
