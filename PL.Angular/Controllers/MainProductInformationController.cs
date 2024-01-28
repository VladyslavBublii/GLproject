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
        private readonly ISettings _settings;

        public MainProductsInformationController(IMainProductInformationService serv, ISettings settings)
        {
            _mainProductService = serv;
            _settings = settings;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMainProductsInformation()
        {
            try
            {
                var GoogleDriveUrl = _settings.GetUrl();

                IEnumerable<MainProductInformationDTO> productDtos = _mainProductService.GetProducts();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MainProductInformationDTO, MainProductInformation>()).CreateMapper();
                var mainProductsInformation = mapper.Map<IEnumerable<MainProductInformationDTO>, List<MainProductInformation>>(productDtos);
                mainProductsInformation.ForEach(product => product.GoogleUrl = GoogleDriveUrl);

                return Ok(mainProductsInformation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
