using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void AddItem(Guid idItem, Guid userId)
        {
            try
            {
                if (_unitOfWork.Products.Get(idItem) == null) throw new Exception("Product not found");
            }
            catch { };

            Cart ithem = new Cart
            {
                ProductsId = idItem,
                UserId     = userId,
            };

            _unitOfWork.Carts.Create(ithem);
            _unitOfWork.Save();
        }
        public bool CheckItem(Guid idItem)
        {
            try
            {
                var product = _unitOfWork.Products.Get(idItem);
                if (product != null) return true;
            }
            catch { };
            return false;
        }

        public void RemoveItem(Guid userId, Guid productId)
        {
            var ithemCart = _unitOfWork.Carts.GetAll().Where(x => x.ProductsId == productId && x.UserId == userId ).Select(x => x.Id).First();
            _unitOfWork.Carts.Delete(ithemCart);
            _unitOfWork.Save();
        }

        public decimal ComputeTotalValue(IEnumerable<Guid> ithemIds)
        {
            decimal sum = 0;

            foreach(var ithemId in ithemIds)
            {
                sum += _unitOfWork.Products.GetAll().Where(x => x.Id == ithemId).Select(x => x.Price).First();
            }

            return sum;
        }
        public void Clear(Guid userId)
        {
            var products = _unitOfWork.Carts.GetAll().Where(x => x.UserId == userId).Select(x => x.ProductsId);

            foreach(var product in products)
            {
                _unitOfWork.Carts.Delete(product);
            }

            _unitOfWork.Save();
        }

        public CartDTO ShowCart(Guid userId)
        {
            using var unit = new UnitOfWork();
            var cartsId = unit.Carts.GetAll().Where(x => x.UserId == userId).Select(x => x.ProductsId);

            CartDTO cartDTO = new CartDTO();
            List<ProductDTO> productsInCart = new List<ProductDTO>();

            var products = unit.Products.GetAll();
            foreach(var cartId in cartsId)
            {
                foreach(var product in products)
                {
                    if(product.Id == cartId)
                    {
                        productsInCart.Add(new ProductDTO
                        {
                            Id          = product.Id,
                            Price       = product.Price,
                            Category    = product.Category,
                            Name        = product.Name,
                            Description = product.Description,
                            Image       = product.Image,
                        });
                        cartDTO.Sum += product.Price;
                    }
                }
            }

            cartDTO.Products = productsInCart;
            cartDTO.Id       = Guid.NewGuid();
            cartDTO.UserId   = userId;

            return cartDTO;
        }

        public void MakeOrder(Guid userId)
        {
            try
            {
                var listOfProducts = TakeIthemFromCart(userId);

                Order order = new Order
                {
                    UserId = userId,
                    Sum = ComputeTotalValue(listOfProducts),
                    OrderTime  = DateTime.UtcNow,
                    Status = OrderStatus.Open
                };

                _unitOfWork.Orders.Create(order);
                _unitOfWork.Save();
            }
            catch { }

        }
        private IEnumerable<Guid> TakeIthemFromCart(Guid userId)
        {
            return  _unitOfWork.Carts.GetAll().Where(x => x.UserId == userId).Select(x => x.ProductsId);
        }
    }
}