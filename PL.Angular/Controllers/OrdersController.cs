using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using PL.Angular.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrdersController(IOrderService orderService, IS3Bucket bucket) : Controller
    {
        [HttpPost("getByUserId")]
        public async Task<IActionResult> GetOrdersByUserId([FromBody] string userId)
        {
            var orderDtos = await orderService.GetOrdersByUserIdAsync(Guid.Parse(userId));
            var s3Bucket = bucket;
            var mapper = new MapperConfiguration(cfg =>
            {
                //TODO: Update, when we have complex mapper
                cfg.CreateMap<ProductDTO, OrderedProduct>();
                cfg.CreateMap<OrderDTO, OrderModel>()
                    .AfterMap((orderDtos,orders) =>
                    {
                        orders.OrderedProducts = orderDtos.Products
                        .GroupBy(product => product.Id)
                        .Select(group => new OrderedProduct
                        {
                            Id = group.Key,
                            Name = group.First().Name,
                            Description = group.First().Description,
                            Category = group.First().Category,
                            Price = group.First().Price,
                            Count = (uint)group.Count(),
                            ImageName = group.First().ImageName,
                            UrlImage = s3Bucket.GetImageLink(group.First().ImageName)
                        }).ToList();
                    });
            }).CreateMapper();
            var orderList = mapper.Map<IEnumerable<OrderDTO>, List<OrderModel>>(orderDtos);

            return Ok(orderList);
        }

        [HttpPost("makeOrder")]
        public async Task<IActionResult> MakeOrder([FromBody] List<OrderRequestModel> orderList)
        {
            try
            {
                var orderDto = new OrderDTO 
                { 
                    UserId = Guid.Parse(orderList.FirstOrDefault()?.UserId),
                    ProductIds = new List<Guid>()
                };
                foreach (var order in orderList)
                {
                    foreach (int counter in Enumerable.Range(1, (int)order.Count))
                    {
                        orderDto.ProductIds.Add(Guid.Parse(order.ProductId));
                    }
                }
                await orderService.MakeOrderAsync(orderDto);
                return Ok(orderList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
