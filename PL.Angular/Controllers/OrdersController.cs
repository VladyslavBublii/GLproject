using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using PL.Angular.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrdersController : Controller
    {
        private IOrderService _orderService;
        private readonly IS3Bucket _s3Bucket;

        public OrdersController(IOrderService orderService, IS3Bucket s3Bucket)
        {
            _orderService = orderService;
            _s3Bucket = s3Bucket;
        }

        [HttpPost("getByUserId")]
        public async Task<IActionResult> GetOrdersByUserId([FromBody] string userId)
        {
            IEnumerable<OrderDTO> orderDtos = _orderService.GetOrdersByUserId(Guid.Parse(userId));
            var s3Bucket = _s3Bucket;
            var mapper = new MapperConfiguration(cfg =>
            {
                //TODO: Update, when we have complex mapper
                cfg.CreateMap<ProductDTO, OrderedProduct>();
                cfg.CreateMap<OrderDTO, OrderModel>()
                    .AfterMap((orderDtos,orders) =>
                    {
                        orders.OrderedProducts = orderDtos.Products
                        .Select(product => 
                        {
                            var count = orderDtos.ProductIds
                                .Count(productId => productId == product.Id);
                            return new OrderedProduct
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Category = product.Category,
                                Price = product.Price,
                                Count = (uint)count,
                                ImageName = product.ImageName,
                                UrlImage = s3Bucket.GetImageLink(product.ImageName)
                            };
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
                _orderService.MakeOrder(orderDto);
                return Ok(orderList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
