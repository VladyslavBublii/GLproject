using BL.Services.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using System;
using System.Collections.Generic;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("getBasket")]
        public async Task<IActionResult> GetBasket([FromBody] string userId)
        {
            var products = _cartService.ShowCart(Guid.Parse(userId));
            if (products == null || !products.Products.Any()) 
            {
                return NotFound();
            }
            List<CartModel> cartModels = new List<CartModel>();

            foreach (var product in products.Products)
            {
                var x = cartModels.Find(x => x.Id == product.Id);

                if(x != null) { x.Count++; }
                else
                {
                    CartModel cartModel = new CartModel
                    {
                        Id          = product.Id,
                        Name        = product.Name,
                        Description = product.Description,
                        Category    = product.Category,
                        Price       = product.Price,
                        Count       = 1
                    };
                    cartModels.Add(cartModel);
                }
            }

            return Ok(cartModels);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] string productId)
        {
            string userId = ""; //current registered user id

            if (!_cartService.CheckItem(Guid.Parse(productId)))
            {
                return NotFound();
            }
            _cartService.AddItem(Guid.Parse(productId), Guid.Parse(userId));

            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] Guid productId)
        {
            string userId = ""; //current registered user id
            _cartService.RemoveItem(Guid.Parse(userId), productId);

            return Ok();
        }
    }
}
