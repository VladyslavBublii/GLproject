using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public decimal Name { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public decimal Surname { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public decimal PhoneNumber { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public string City { get; set; } // Пока вместо отдельной таблицы UsersDetails
        public string PostIndex { get; set; } // Пока вместо отдельной таблицы UsersDetails

        public int ProductId { get; set; }
    }
}
