using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;

namespace PL.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;

        public ProductsController(IProductService serv)
        {
            _productService = serv;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
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

        public ActionResult Create()
        {
            return View();

            //try
            //{
            //    var product = new ProductViewModel();

            //    return View(product);
            //}
            //catch (/*Validation*/Exception ex)
            //{
            //    //return Content(ex.Message);
            //}
            //return View(); // Добавлено
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                var productDto = new ProductDTO
                {
                    Name        = product.Name,
                    Category    = product.Category,
                    Description = product.Description,
                    Price       = product.Price,
                };
                _productService.Create(productDto);
                return Content("Product has been added");
            }
            catch (/*Validation*/Exception ex)
            {
                //ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(product);
        }
    }
}
