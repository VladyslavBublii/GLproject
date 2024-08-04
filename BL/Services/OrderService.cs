using System;
using System.Collections.Generic;
using System.Linq;
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

        public void MakeOrder(OrderDTO orderDto)
        {
            decimal sum = 0;

            foreach (var productId in orderDto.ProductIds)
            {
                sum += _unitOfWork.Products.Get(productId).Price;
            }

            Order order = new Order
            {
                UserId      = orderDto.UserId,
                OrderTime   = DateTime.Now,
                City        = orderDto.City,
                PostIndex   = orderDto.PostIndex,
                Sum         = sum,
                PhoneNumber = orderDto.PhoneNumber,
                Status      = OrderStatus.Open
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();

            var recentOrderId = _unitOfWork.OrdersRepository.GetIdByUserIdAndTime(orderDto.UserId, order.OrderTime);

            var productCounts = orderDto.ProductIds.GroupBy(id => id)
                .Select(group => new
                {
                    ProductsId = group.Key,
                    NumberOfProduct = group.Count()
                })
                .ToList();

            var orderProducts = productCounts.Select(pc => new OrderProduct
            {
                OrdersId = recentOrderId,
                ProductsId = pc.ProductsId,
                NumberOfProduct = pc.NumberOfProduct
            }).ToList();

            _unitOfWork.OrdersProducts.AddRangeOrderProduct(orderProducts);
            _unitOfWork.Save();
        }

        public IEnumerable<OrderDTO> GetOrdersByUserId(Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_unitOfWork.OrdersRepository.GetAllByUserId(userId));
        }
        
        public IEnumerable<ProductDTO> GetProducts()
        {
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_unitOfWork.Products.GetAll());

        }

        public ProductDTO GetProduct(Guid id)
        {
            var product = _unitOfWork.Products.Get(id);
            return new ProductDTO { Description = product.Description, Id = product.Id, Name = product.Name, Price = product.Price };
        }
    }
}
