using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddItemAsync(Guid idItem, Guid userId)
        {
            var product = await _unitOfWork.Products.GetAsync(idItem);
            if (product == null)
                throw new Exception("Product not found");

            var cartItem = new Cart
            {
                ProductsId = idItem,
                UserId = userId
            };

            await _unitOfWork.Carts.CreateAsync(cartItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> CheckItemAsync(Guid idItem)
        {
            var product = await _unitOfWork.Products.GetAsync(idItem);
            return product != null;
        }

        public async Task RemoveItemAsync(Guid userId, Guid productId)
        {
            var cartItems = await _unitOfWork.Carts.GetAllAsync();

            var cartItem = cartItems.FirstOrDefault(x => x.ProductsId == productId && x.UserId == userId);

            if (cartItem != null)
            {
                _unitOfWork.Carts.Delete(cartItem);
                await _unitOfWork.SaveAsync();
            }
        }


        public async Task<decimal> ComputeTotalValueAsync(IEnumerable<Guid> itemIds)
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var totalSum = products
                .Where(product => itemIds.Contains(product.Id))
                .Sum(product => product.Price);

            return totalSum;
        }


        public async Task ClearAsync(Guid userId)
        {
            var cartItems = (await _unitOfWork.Carts
                .GetAllAsync())
                .Where(x => x.UserId == userId)
                .ToList();

            _unitOfWork.Carts.DeleteRange(cartItems);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CartDTO> ShowCartAsync(Guid userId)
        {
            var cartItems = (await _unitOfWork.Carts
                .GetAllAsync())
                .Where(cart => cart.UserId == userId)
                .ToList();

            var productIds = cartItems.Select(cart => cart.ProductsId).ToList();

            var products = await _unitOfWork.Products
                .GetAsync(productIds);

            var productDTOs = products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Price = product.Price,
                Category = product.Category,
                Name = product.Name,
                Description = product.Description,
                ImageName = product.ImageName
            }).ToList();

            return new CartDTO
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = productDTOs,
                Sum = productDTOs.Sum(p => p.Price)
            };
        }

        public async Task MakeOrderAsync(Guid userId)
        {
            var productIds = await TakeItemsFromCartAsync(userId);

            var order = new Order
            {
                UserId = userId,
                Sum = await ComputeTotalValueAsync(productIds),
                OrderTime = DateTime.UtcNow,
                Status = OrderStatus.Open
            };

            await _unitOfWork.Orders.CreateAsync(order);
            await _unitOfWork.SaveAsync();
        }

        private async Task<IEnumerable<Guid>> TakeItemsFromCartAsync(Guid userId)
        {
            var cartItems = await _unitOfWork.Carts.GetAllAsync();
            return cartItems.Where(cart => cart.UserId == userId) 
                            .Select(cart => cart.ProductsId); 
        }

    }
}