using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using PL.Models;
using System.IO;

namespace PL.Controllers
{
    public class AdminController : Controller
    {
        IProductService _productService;

        public AdminController(IProductService serv)
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

            //TODO: Exeption
            catch (/*Validation*/Exception ex)
            {
                //return Content(ex.Message);
                return View();
            }
        }

        public ViewResult Edit(Guid Id)
        {
            ProductDTO productDto = _productService.Find(Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var product = mapper.Map<ProductDTO, ProductViewModel>(productDto);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {

            ProductDTO productDto = _productService.Find(productViewModel.Id);
            {
                productDto.Id = productViewModel.Id;
                productDto.Name = productViewModel.Name;
                productDto.Category = productViewModel.Category;
                productDto.Description = productViewModel.Description;
                productDto.Price = productViewModel.Price;
            }

            if (productViewModel.ImageIn != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(productViewModel.ImageIn.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)productViewModel.ImageIn.Length);
                }
                productViewModel.Image = imageData;
                productDto.Image = productViewModel.Image;
            }

            if (ModelState.IsValid)
            {
                _productService.Update(productDto);
                TempData["message"] = string.Format("Changes in the  \"{0}\" have been saved", productDto.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных вместо Exeption?
                return View(productViewModel);
            }
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
				ProductDTO productDto = new ProductDTO
                {
                    Name        = productViewModel.Name,
                    Category    = productViewModel.Category,
                    Description = productViewModel.Description,
                    Price       = productViewModel.Price,
                };

                byte[] imageData = null;

                if (productViewModel.ImageIn != null)
				{
					using (var binaryReader = new BinaryReader(productViewModel.ImageIn.OpenReadStream()))
					{
						imageData = binaryReader.ReadBytes((int)productViewModel.ImageIn.Length);
					}
                }
                productViewModel.Image = imageData;
                productDto.Image = productViewModel.Image;

                //TODO: установка ID не помогает пройти ModelState.IsValid
                //productDto.Id = Guid.NewGuid();

                //if (ModelState.IsValid)
                //{
                _productService.Create(productDto);
                TempData["message"] = string.Format("The  \"{0}\" has been added", productDto.Name);
                return RedirectToAction("Index");
			    //}

			    //	else
			    //{
			    //	// Что-то не так со значениями данных
			    //	return View(productViewModel);
			    //}
		    }

            //TODO: Exeption
            catch (/*Validation*/Exception ex)
            {
                //ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
             var deletedProduct = _productService.Delete(id);
             if (deletedProduct != null)
             {
                TempData["message"] = string.Format("The \"{0}\" has been deleted", deletedProduct.Name);
             }
        return RedirectToAction("Index");
        }     
    }
}

