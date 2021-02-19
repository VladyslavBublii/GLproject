using AutoMapper;
using BL.DTO;
using BL.Infrastructure;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        IOrderService orderService;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductDTO> productDtos = orderService.GetPhones();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            return View(products);
        }

        public ActionResult MakeOrder(int? id)
        {
            try
            {
                ProductDTO phone = orderService.GetPhone(id);
                var order = new OrderViewModel { ProductId = phone.Id };

                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                var orderDto = new OrderDTO { ProductId = order.ProductId, PostIndex = order.PostIndex, PhoneNumber = order.PhoneNumber };
                orderService.MakeOrder(orderDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }

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
