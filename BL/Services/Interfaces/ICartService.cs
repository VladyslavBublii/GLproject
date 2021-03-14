using Core.Models;
using System;
using System.Collections.Generic;

namespace BL.Services.Interfaces
{
    public interface ICartService
    {
        public void AddItem(Guid idItem);
        public void RemoveItem(Cart product);
        public bool CheckItem(Guid idItem);
        public decimal ComputeTotalValue();
        public void Clear();
        public IEnumerable<Cart> ShowCart();
        public void MakeOrder(Guid userId);
    }
}
