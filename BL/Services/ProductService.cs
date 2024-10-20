using System;
using System.Collections.Generic;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace BL.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Create(ProductDTO productDTO)
        {
            Product product = new Product
            {
                Name        = productDTO.Name,
                Category    = productDTO.Category,
                Description = productDTO.Description,
                Price       = productDTO.Price,
                ImageName   = productDTO.ImageName,
            };

            _unitOfWork.Products.Create(product);
            _unitOfWork.Save();
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_unitOfWork.Products.GetAll());
        }

        public ProductDTO GetProduct(Guid id)
        {
            //TODO: Exception
            //if (id == null)
            //    throw new ValidationException("Не установлено id товара", "");
            //if (product == null)
            //    throw new ValidationException("Товар не найден", "");
            var product = _unitOfWork.Products.Get(id);
            return new ProductDTO { Id = product.Id, ImageName = product.ImageName, 
                Name = product.Name, Category = product.Category, Description = product.Description, 
                Price = product.Price, };
        }

        public void Update(ProductDTO productDTO)
        {

            Product dbEntry = _unitOfWork.Products.Find(productDTO.Id);
            if (dbEntry != null)
            {
                dbEntry.Name        = productDTO.Name;
                dbEntry.Category    = productDTO.Category;
                dbEntry.Description = productDTO.Description;
                dbEntry.Price       = productDTO.Price;
                dbEntry.ImageName   = productDTO.ImageName;
            }
            _unitOfWork.Products.Update(dbEntry);
            _unitOfWork.Save();
        }

        public ProductDTO Find(Guid id)
        {
            var product = _unitOfWork.Products.Find(id);
            return new ProductDTO { Id = product.Id, ImageName = product.ImageName, 
                Name = product.Name, Category = product.Category, Description = product.Description, 
                Price = product.Price, };
        }

        public ProductDTO Delete(Guid id)
        {
            var product = _unitOfWork.Products.Find(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product.Id);
                _unitOfWork.Save();
            }
            return new ProductDTO { Id = product.Id, ImageName = product.ImageName, 
                Name = product.Name, Category = product.Category, Description = product.Description, 
                Price = product.Price, };

        }

        public bool CheckItem(Guid idItem)
        {
            try
            {
                var product = _unitOfWork.Products.Get(idItem);
                if (product != null) return true;
            }
            catch { };
            return false;
        }
        //TODO:
        //public void Dispose()
        //{
        //    _unitOfWork.Dispose();
        //}
    }
}
