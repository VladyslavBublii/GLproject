using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly List<Product> Products = new List<Product>();

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

            var product = _unitOfWork.Products.Get(idItem);

            Cart ithem = new Cart
            {
                ProductsId = idItem,
                UserId     = userId,
            };

            _unitOfWork.Cart.Create(ithem);
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

        public void RemoveItem(Cart product)
        {
            //Products.RemoveAll(l => l == product);
        }

        public decimal ComputeTotalValue()
        {
            decimal sum = 0;

            foreach (var product in Products)
            {
                sum += product.Price;
            }

            return sum;

        }
        public void Clear()
        {
            Products.Clear();
        }

        public IEnumerable<Product> ShowCart(Guid userId)
        {
            var cartsId = _unitOfWork.Cart.GetAll().Where(x => x.UserId == userId).Select(x => x.ProductsId).First();

            //List<Product> products = new List<Product>();

            var products = _unitOfWork.Products.Get(cartsId);
            yield return products;
        }

        public void MakeOrder(Guid userId)
        {
            try
            {
                var user = _unitOfWork.Users.Get(userId);

                Order order = new Order
                {
                    Id       = new Guid(),
                    Customer = user.Customer,
                    Sum      = ComputeTotalValue(),
                    Products = this.Products,
                    Date     = DateTime.Now,
                };

                _unitOfWork.Orders.Create(order);
                _unitOfWork.Save();
            }
            catch { }

        }
    }
}