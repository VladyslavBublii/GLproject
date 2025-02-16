using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController(IProductService productService, IS3Bucket s3Bucket) : ControllerBase
    {
        private readonly IS3Bucket _s3Bucket = s3Bucket;

        [HttpPost("add")]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductModel productRequestModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductModel, ProductDTO>()).CreateMapper();
            var productDto = mapper.Map<ProductDTO>(productRequestModel);

            await productService.CreateAsync(productDto);

            return Ok(productRequestModel);
        }
    }
}
