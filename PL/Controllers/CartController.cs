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
    [Authorize(Roles = "admin, user")]
    public class CartController : Controller
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //[Authorize(Roles = "admin, user")]
        public ActionResult Index()
        {
            string userId = User.Identity.Name;

            var products = _cartService.ShowCart(Guid.Parse(userId));
            if (products == null) return NotFound();
            List<CartModel> cartModels = new List<CartModel>();

            //var sum = products.Sum;
            foreach (var product in products.Products)
            {
                var x = cartModels.Find(x => x.Id == product.Id);

                if(x != null) { x.Count++; }
                else
                {
                    CartModel cartModel = new CartModel
                    {
                        Id = product.Id,
                        ProductId = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Category = product.Category,
                        Price = product.Price,
                        Count = 1
                    };
                    cartModels.Add(cartModel);
                }

            }

            return View(cartModels);
        }

        public IActionResult AddToCart(string id)
        {
            string userId = User.Identity.Name;

            if (!_cartService.CheckItem(Guid.Parse(id)))
            {
                return NotFound();
            }
            _cartService.AddItem(Guid.Parse(id), Guid.Parse(userId));

            return Ok();
        }

        public void RemoveFromCart(Guid productId)
        {
            _cartService.RemoveItem(productId);
        }

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
