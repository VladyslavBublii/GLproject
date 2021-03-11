using Core.Models;
using System;
using System.Collections.Generic;

namespace BL.Services.Interfaces
{
    public interface ICartService
    {
        public void AddItem(Product product, int quantity);
        public void RemoveItem(Product product);
        public bool CheckItem(Guid idItem);
        public decimal ComputeTotalValue();
        public void Clear();
        public List<Product> ShowCart();
        public void MakeOrder(Guid userId);
    }
}
