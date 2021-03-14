using BL.Services.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            //ProductViewModel
        }

        //[Authorize(Roles = "admin, user")]
        public ActionResult Index()
        {
            var products = _cartService.ShowCart();
            if (products == null) return NotFound();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Id          = product.Id
                };
                productViewModels.Add(productViewModel);
            }

            return View(productViewModels);
        }

        public IActionResult AddToCart(string id)
        {
            string userId = User.Identity.Name;

            if (!_cartService.CheckItem(Guid.Parse(id)))
            {
                return NotFound();
            }
            _cartService.AddItem(Guid.Parse(id));

            return Ok();
        }

        //public IActionResult RemoveFromCart(Product product)
        //{
        //    _cartService.RemoveItem(product);
        //}

        //public IActionResult ComputeTotalValue()
        //{
        //    _cartService.ComputeTotalValue();
        //}
        //public IActionResult MakeOrder(Guid userId)
        //{
        //    _cartService.MakeOrder(userId);
        //}
    }
}
