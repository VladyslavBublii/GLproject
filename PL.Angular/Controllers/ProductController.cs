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
            var productDto = new ProductDTO();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductModel>()).CreateMapper();
            var productModel = mapper.Map<ProductModel>(productDto);
            _productService.Create(productDto);

            return Ok(productRequestModel);
        }
    }
}
