﻿using System;
using System.Collections.Generic;

namespace BL.DTO
{
	public class OrderDTO
    {
        public Guid Id { get; set; }
        public decimal Sum { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostIndex { get; set; }

        public Guid UserId { get; set; }
        public ICollection<Guid> ProductIds { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
        public DateTime Date { get; set; }
    }
}
