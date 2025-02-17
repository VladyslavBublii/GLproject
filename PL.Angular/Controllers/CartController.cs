using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController(ICartService cartService, IS3Bucket s3Bucket) : ControllerBase
    {
        [HttpPost("getBasket")]
        public async Task<IActionResult> GetBasket([FromBody] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            var cart = await cartService.ShowCartAsync(userGuid);

            if (cart?.Products == null || !cart.Products.Any())
            {
                return NotFound("Cart is empty or user not found.");
            }

            var cartModels = cart.Products
                .GroupBy(product => product.Id)
                .Select(group => new CartModel
                {
                    Id = group.Key,
                    Name = group.First().Name,
                    Description = group.First().Description,
                    Category = group.First().Category,
                    Price = group.First().Price,
                    Count = (uint)group.Count(),
                    ImageName = group.First().ImageName,
                    UrlImage = s3Bucket.GetImageLink(group.First().ImageName)
                })
                .ToList();

            return Ok(cartModels);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequestModel cartRequestModel)
        {
            if (cartRequestModel == null ||
                !Guid.TryParse(cartRequestModel.ProductId, out var productGuid) ||
                !Guid.TryParse(cartRequestModel.UserId, out var userGuid))
            {
                return BadRequest("Invalid input.");
            }

            var isProductExists = await cartService.CheckItemAsync(productGuid);
            if (!isProductExists)
            {
                return NotFound("Product not found.");
            }

            await cartService.AddItemAsync(productGuid, userGuid);

            return Ok(new { message = "Product added to cart." });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] CartRequestModel cartRequestModel)
        {
            if (cartRequestModel == null ||
                !Guid.TryParse(cartRequestModel.ProductId, out var productGuid) ||
                !Guid.TryParse(cartRequestModel.UserId, out var userGuid))
            {
                return BadRequest("Invalid input.");
            }

            await cartService.RemoveItemAsync(userGuid, productGuid);

            return Ok(new { message = "Product removed from cart." });
        }
    }
}
