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

namespace BL.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task MakeOrderAsync(OrderDTO orderDto)
    {
        decimal sum = 0;
        foreach (var productId in orderDto.ProductIds)
        {
            var product = await unitOfWork.Products.GetAsync(productId);
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

        await unitOfWork.Orders.CreateAsync(order);
        await unitOfWork.SaveAsync();

        var recentOrderId = await unitOfWork.Orders.GetIdByUserIdAndTimeAsync(orderDto.UserId, order.OrderTime);

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

        await unitOfWork.OrdersProducts.AddRangeOrderProductAsync(orderProducts);
        await unitOfWork.SaveAsync();

        var carts = await unitOfWork.Carts
            .GetAllAsync();
        var userCarts = carts.Where(cart => cart.UserId == orderDto.UserId).ToList();

        unitOfWork.Carts.DeleteRange(userCarts);
        await unitOfWork.SaveAsync();
    }


    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(Guid userId)
    {
        var mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Order, OrderDTO>();
            cfg.CreateMap<Product, ProductDTO>();
        }).CreateMapper();

        var orders = await unitOfWork.Orders.GetAllByUserIdAsync(userId);
        var ordersDto = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);

        foreach (var order in ordersDto)
        {
            var ordersProductsList = await unitOfWork.OrdersProducts.GetOrderProductsByOrderIdAsync(order.Id);

            order.ProductIds = ordersProductsList
                .SelectMany(op => Enumerable.Repeat(op.ProductsId, op.NumberOfProduct))
                .ToList();

            var products = await unitOfWork.Products.GetAsync(order.ProductIds);

            order.Products = mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        return ordersDto;
    }


    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var products = await unitOfWork.Products.GetAllAsync();
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductAsync(Guid id)
    {
        var product = await unitOfWork.Products.GetAsync(id);

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