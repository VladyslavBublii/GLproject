using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
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

        public void AddItem(Guid idItem)
        {
            try
            {
                if (_unitOfWork.Products.Get(idItem) == null) throw new Exception("Product not found");
            }
            catch { };

            var products = _unitOfWork.Products.Get(idItem);
            _unitOfWork.Cart.Create(products);
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

        public void RemoveItem(Product product)
        {
            Products.RemoveAll(l => l == product);
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

        public IEnumerable<Product> ShowCart()
        {
            return _unitOfWork.Cart.GetAll();
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