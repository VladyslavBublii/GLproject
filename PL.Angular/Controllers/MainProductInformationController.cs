using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("store")]
    public class MainProductsInformationController(
        IMainProductInformationService mainProductService,
        IS3Bucket s3Bucket,
        IMapper mapper)
        : ControllerBase
    {
        [HttpGet("get")]
        public async Task<IActionResult> GetMainProductsInformation()
        {
            try
            {
                var productDtos = await mainProductService.GetProductsAsync();

                if (productDtos == null || !productDtos.Any())
                {
                    return NotFound("No products found.");
                }

                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MainProductInformationDTO, MainProductInformation>()
                        .ForMember(dest => dest.UrlImage, opt => opt.Ignore());
                });

                var mapper = mapperConfig.CreateMapper();

                var mainProductsInformationList = mapper.Map<IEnumerable<MainProductInformation>>(productDtos);

                foreach (var product in mainProductsInformationList)
                {
                    product.UrlImage = s3Bucket.GetImageLink(product.ImageName);
                }

                return Ok(mainProductsInformationList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
