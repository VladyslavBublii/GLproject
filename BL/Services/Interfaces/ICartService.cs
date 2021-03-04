using Core.Models;
using System;

namespace BL.Services.Interfaces
{
    public interface ICartService
    {
        public void AddItem(Product product, int quantity);
        public void RemoveItem(Product product);
        public decimal ComputeTotalValue();
        public void Clear();
        public void MakeOrder(Guid userId);
    }
}
