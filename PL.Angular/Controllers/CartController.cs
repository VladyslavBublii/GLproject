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
        public async Task<IActionResult> AddToCart([FromBody] CartRequestModel cartRequestModel)
        {
            if (!_cartService.CheckItem(Guid.Parse(cartRequestModel.ProductId)))
            {
                return NotFound();
            }
            _cartService.AddItem(Guid.Parse(cartRequestModel.ProductId), Guid.Parse(cartRequestModel.UserId));

            return Ok(cartRequestModel);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] CartRequestModel cartRequestModel)
        {
            _cartService.RemoveItem(Guid.Parse(cartRequestModel.UserId), Guid.Parse(cartRequestModel.ProductId));

            return Ok(cartRequestModel);
        }
    }
}
