using System.Collections.Generic;
using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using BL.Services.Interfaces;
using PL.Models;
using AutoMapper;

namespace PL.Controllers
{
	public class OrdersController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        IOrderService _orderService;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public OrdersController(IOrderService serv)
        {
            _orderService = serv;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<ProductDTO> productDtos = _orderService.GetProducts();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
                var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
                return View(products);
            }
            catch (/*Validation*/Exception ex)
            {
                //return Content(ex.Message);
                return View(); // Добавлено
            }

        }

        public ActionResult MakeOrder(Guid id)
        {
            try
            {
                ProductDTO product = _orderService.GetProduct(id);
                var order = new OrderViewModel { ProductId = product.Id };

                return View(order);
            }
            catch (/*Validation*/Exception ex)
            {
				//return Content(ex.Message);
			}
            return View(); // Добавлено
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                var orderDto = new OrderDTO { ProductId = order.ProductId, PostIndex = order.PostIndex, PhoneNumber = order.PhoneNumber };
                _orderService.MakeOrder(orderDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (/*Validation*/Exception ex)
            {
                //ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    _orderService.Dispose();
        //    base.Dispose(disposing);
        //}

        /*
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}
