using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Category { get; set; } // Пока вместо отдельной таблицы
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
