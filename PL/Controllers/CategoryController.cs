using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
	public class CategoryController : Controller
	{
        IProductService _productService;

        public CategoryController(IProductService serv)
        {
            _productService = serv;
        }

        public string Menu()
        {
            return "Тестируем контроллер Nav";
        }

        public PartialViewResult CategoryMenu()
        {

            IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);

            IEnumerable<string> categories = products
                .Select(product => product.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}
