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

        public MainProductsInformationController(IMainProductInformationService serv)
        {
            _mainProductService = serv;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMainProductsInformation()
        {
            try
            {
                IEnumerable<MainProductInformationDTO> productDtos = _mainProductService.GetProducts();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MainProductInformationDTO, MainProductInformation>()).CreateMapper();
                var mainProductsInformation = mapper.Map<IEnumerable<MainProductInformationDTO>, List<MainProductInformation>>(productDtos);
                return Ok(mainProductsInformation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
