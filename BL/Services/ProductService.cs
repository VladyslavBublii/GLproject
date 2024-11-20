using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;

namespace BL.Services
{
    public class ProductService : IProductService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Category = productDTO.Category,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ImageName = productDTO.ImageName,
            };

            await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            var dbEntry = await _unitOfWork.Products.FindAsync(productDTO.Id);
            if (dbEntry == null)
                throw new KeyNotFoundException("Product not found for update");

            dbEntry.Name = productDTO.Name;
            dbEntry.Category = productDTO.Category;
            dbEntry.Description = productDTO.Description;
            dbEntry.Price = productDTO.Price;
            dbEntry.ImageName = productDTO.ImageName;

            await _unitOfWork.Products.UpdateAsync(dbEntry);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ProductDTO> FindAsync(Guid id)
        {
            var product = await _unitOfWork.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> DeleteAsync(Guid id)
        {
            var product = await _unitOfWork.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> CheckItemAsync(Guid idItem)
        {
            try
            {
                var product = await _unitOfWork.Products.GetAsync(idItem);
                return product != null;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
