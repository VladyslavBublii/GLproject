using BL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface ICartService
    {
        Task AddItemAsync(Guid idItem, Guid userId);

        Task RemoveItemAsync(Guid userId, Guid productId);

        Task<bool> CheckItemAsync(Guid idItem);

        Task<decimal> ComputeTotalValueAsync(IEnumerable<Guid> itemIds);

        Task<CartDTO> ShowCartAsync(Guid userId);

        Task MakeOrderAsync(Guid userId);

        Task ClearAsync(Guid userId);
    }
}
