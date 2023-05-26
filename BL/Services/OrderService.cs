using System;
using System.Collections.Generic;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace BL.Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork _unitOfWork;

        public OrderService()
        {
            _unitOfWork = new UnitOfWork();
        }

        //IUnitOfWork Database { get; set; }

        //public OrderService(IUnitOfWork uow)
        //{
        //    Database = uow;
        //}
        public void MakeOrder(OrderDTO orderDto)
        {
            Product product = _unitOfWork.Products.Get(orderDto.ProductId);

            // валидация
            //if (product == null)
            //    throw new ValidationException("Товар не найден", "");

            // применяем скидку
            //decimal sum = new Discount(0.1m).GetDiscountedPrice(product.Price);

            Order order = new Order
            {
                OrderTime = DateTime.Now,
                City        = orderDto.City,
                PostIndex   = orderDto.PostIndex, 
                Sum         = product.Price,
                PhoneNumber = orderDto.PhoneNumber,
                Status = OrderStatus.Open
            };
            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();
        }

        //TODO: Проверить реализацию интерфейса
        public IEnumerable<ProductDTO> GetProducts()
        {
			//IEnumerable<ProductDTO> products = _unitOfWork.Products.GetAll() as IEnumerable<ProductDTO>;
			// применяем автомаппер для проекции одной коллекции на другую
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_unitOfWork.Products.GetAll());
			//return products;

        }

        public ProductDTO GetProduct(Guid id)
        {
            //if (id == null)
            //    throw new ValidationException("Не установлено id товара", "");
            var product = _unitOfWork.Products.Get(id);
            //if (product == null)
            //    throw new ValidationException("Товар не найден", "");

            return new ProductDTO { Description = product.Description, Id = product.Id, Name = product.Name, Price = product.Price };
        }

        //public void Dispose()
        //{
        //    _unitOfWork.Dispose();
        //}
    }
}
