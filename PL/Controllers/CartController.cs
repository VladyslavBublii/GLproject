using BL.Services.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PL.Controllers
{
    public class CartController
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //public IActionResult AddToCart(Product product, int quantity)
        //{
        //    _cartService.AddItem(product, quantity);
        //    return IActionResult("Index");
        //}

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
