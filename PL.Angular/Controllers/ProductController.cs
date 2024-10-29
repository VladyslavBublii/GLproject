using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IS3Bucket _s3Bucket;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IS3Bucket s3Bucket) 
        {
            _productService = productService;
            _s3Bucket = s3Bucket;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductModel productRequestModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductModel, ProductDTO>()).CreateMapper();
            var productDto = mapper.Map<ProductDTO>(productRequestModel);

            _productService.Create(productDto);

            return Ok(productRequestModel);
        }
    }
}
