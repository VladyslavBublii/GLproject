using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using Microsoft.EntityFrameworkCore;

namespace PL.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;
        public int pageSize = 5;

        public ProductsController(IProductService serv)
        {
            _productService = serv;
        }

        public IActionResult Index(string category, int page = 1)
        {

            IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            //return View(products);

            var count = products.Count();
            var productsPage = products
                .Where(p => category == null || p.Category == category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                ProductViewModels = productsPage,
                CurrentCategory = category
            };
            return View(viewModel);
        }

   //     public async Task<IActionResult> IndexSort(SortState sortOrder = SortState.NameAsc)
   //     {
   //         IEnumerable<ProductDTO> productDtos = _productService.GetProducts();
   //         var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
   //         var productsm = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
   //         IQueryable<ProductDTO> products = productsm as IQueryable<ProductDTO>;


   //         ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
   //         ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
   //         ViewData["CategorySort"] = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;

   //         products = sortOrder switch
   //         {
   //             SortState.NameAsc => products.OrderBy(s => s.Name),
   //             SortState.NameDesc => products.OrderByDescending(s => s.Name),
   //             SortState.PriceAsc => products.OrderBy(s => s.Price),
   //             SortState.PriceDesc => products.OrderByDescending(s => s.Price),
   //             SortState.CategoryAsc => products.OrderBy(s => s.Category),
   //             SortState.CategoryDesc => products.OrderByDescending(s => s.Category),
			//	_ => products.OrderBy(s => s.Name),
			//};
   //         return View(await products.AsNoTracking().ToListAsync());
   //     }

        public ActionResult Create()
        {
            return View();
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
