using AutoMapper;
using BL.DTO;
using BL.Services;
using BL.Services.Interfaces;
using PL.Angular.Models;
using Microsoft.AspNetCore.Mvc;
using Core.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrdersController : Controller
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("getByUserId")]
        public async Task<IActionResult> GetOrdersByUserId([FromBody] string userId)
        {
            IEnumerable<OrderDTO> orderDtos = _orderService.GetOrdersByUserId(Guid.Parse(userId));
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderModel>()).CreateMapper();
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
