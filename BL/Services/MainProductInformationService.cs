using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services;

public class MainProductInformationService(IUnitOfWork unitOfWork) : IMainProductInformationService
{
    public async Task<IEnumerable<MainProductInformationDTO>> GetProductsAsync()
    {
        var products = await unitOfWork.Products.GetAllAsync();

        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, MainProductInformationDTO>()).CreateMapper();

        return mapper.Map<IEnumerable<Product>, List<MainProductInformationDTO>>(products);
    }
}