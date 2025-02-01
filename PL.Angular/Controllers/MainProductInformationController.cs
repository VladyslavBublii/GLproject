using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("store")]
    public class MainProductsInformationController : ControllerBase
    {
        private readonly IMainProductInformationService _mainProductService;
        private readonly IS3Bucket _s3Bucket;
        private readonly IMapper _mapper;

        public MainProductsInformationController(IMainProductInformationService mainProductService, IS3Bucket s3Bucket, IMapper mapper)
        {
            _mainProductService = mainProductService;
            _s3Bucket = s3Bucket;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMainProductsInformation()
        {
            try
            {
                var productDtos = await _mainProductService.GetProductsAsync();

                if (productDtos == null || !productDtos.Any())
                {
                    return NotFound("No products found.");
                }

                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MainProductInformationDTO, MainProductInformation>()
                        .ForMember(dest => dest.UrlImage, opt => opt.Ignore());
                });

                var _mapper = mapperConfig.CreateMapper();

                var mainProductsInformationList = _mapper.Map<IEnumerable<MainProductInformation>>(productDtos);

                foreach (var product in mainProductsInformationList)
                {
                    product.UrlImage = _s3Bucket.GetImageLink(product.ImageName);
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
