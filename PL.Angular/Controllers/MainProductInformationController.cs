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
        IMainProductInformationService _mainProductService;
        private readonly IS3Bucket _s3Bucket;

        public MainProductsInformationController(IMainProductInformationService serv, IS3Bucket s3Bucket)
        {
            _mainProductService = serv;
            _s3Bucket = s3Bucket;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMainProductsInformation()
        {
            try
            {
                IEnumerable<MainProductInformationDTO> productDtos = _mainProductService.GetProducts();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MainProductInformationDTO, MainProductInformation>()).CreateMapper();
                var mainProductsInformationList = mapper.Map<IEnumerable<MainProductInformationDTO>, List<MainProductInformation>>(productDtos);
                foreach (var productProductsInformation in mainProductsInformationList) 
                {
                    productProductsInformation.UrlImage = _s3Bucket.GetImageLink(productProductsInformation.ImageName);
                }

                return Ok(mainProductsInformationList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
