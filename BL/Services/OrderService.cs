using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task MakeOrderAsync(OrderDTO orderDto)
        {
            decimal sum = 0;
            foreach (var productId in orderDto.ProductIds)
            {
                var product = await _unitOfWork.Products.GetAsync(productId);
                if (product != null)
                {
                    sum += product.Price;
                }
            }

            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderTime = DateTime.Now,
                City = orderDto.City,
                PostIndex = orderDto.PostIndex,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber,
                Status = OrderStatus.Open
            };

            await _unitOfWork.Orders.CreateAsync(order);
            await _unitOfWork.SaveAsync();

            var recentOrderId = await _unitOfWork.OrdersRepository.GetIdByUserIdAndTimeAsync(orderDto.UserId, order.OrderTime);

            var productCounts = orderDto.ProductIds
                .GroupBy(id => id)
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

            await _unitOfWork.OrdersProducts.AddRangeOrderProductAsync(orderProducts);
            await _unitOfWork.SaveAsync();

            var carts = await _unitOfWork.Carts
                .GetAllAsync();
            var userCarts = carts.Where(cart => cart.UserId == orderDto.UserId).ToList();

            await _unitOfWork.Carts.DeleteRangeAsync(userCarts);
            await _unitOfWork.SaveAsync();
        }


        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Product, ProductDTO>();
            }).CreateMapper();

            var orders = await _unitOfWork.OrdersRepository.GetAllByUserIdAsync(userId);
            var ordersDto = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);

            foreach (var order in ordersDto)
            {
                var ordersProductsList = await _unitOfWork.OrdersProducts.GetOrderProductsByOrderIdAsync(order.Id);

                order.ProductIds = ordersProductsList
                    .SelectMany(op => Enumerable.Repeat(op.ProductsId, op.NumberOfProduct))
                    .ToList();

                var products = await _unitOfWork.Products.GetAsync(order.ProductIds);

                order.Products = mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
            }

            return ordersDto;
        }


        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return new ProductDTO
            {
                Id = product.Id,
                Category = product.Category,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageName = product.ImageName,
            };
        }
    }
}
