using BL.DTO;
using System;
using System.Collections.Generic;

namespace BL.Services.Interfaces
{
    public interface ICartService
    {
        public void AddItem(Guid idItem, Guid userId);

        public void RemoveItem(Guid userId, Guid productId);

        public bool CheckItem(Guid idItem);

        public decimal ComputeTotalValue(IEnumerable<Guid> ithemIds);

        public CartDTO ShowCart(Guid userId);

        public void MakeOrder(Guid userId);

        public void Clear(Guid userId);
    }
}
