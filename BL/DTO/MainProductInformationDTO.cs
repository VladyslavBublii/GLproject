using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class MainProductInformationDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }
    }
}
