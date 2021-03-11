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
            Products.Add(new Product
            {
                Id          = new Guid(),
                Description = "Hi",
                Name        = "Hi",
                Category    = "Hi",
                Price       = 1488,
            });

            _unitOfWork = new UnitOfWork();
        }

        public void AddItem(Product product, int quantity)
        {
            try
            {
                if (_unitOfWork.Products.Get(product.Id) == null) throw new Exception("Product not found");
            }
            catch { };


            for (int i = 0; i < quantity; i++)
            {
                Products.Add(product);
            }
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

        public List<Product> ShowCart()
        {
            return Products;
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