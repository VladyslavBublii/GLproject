using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System.Collections.Generic;

namespace BL.Services
{
    public class MainProductInformationService : IMainProductInformationService
    {
        public IUnitOfWork _unitOfWork;

        public MainProductInformationService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IEnumerable<MainProductInformationDTO> GetProductsAsync()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, MainProductInformationDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<MainProductInformationDTO>>((IEnumerable<Product>)_unitOfWork.Products.GetAllAsync());
        }
    }
}
