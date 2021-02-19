using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public decimal Name { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public decimal Surname { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public decimal PhoneNumber { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public string City { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public string PostIndex { get; set; } // Пока вместо отдельной таблицы UsersDetails

        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
