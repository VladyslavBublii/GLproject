using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;

namespace BL.Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService, IDisposable
{
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

        await unitOfWork.Products.CreateAsync(product);
        await unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var products = await unitOfWork.Products.GetAllAsync();
        return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductAsync(Guid id)
    {
        var product = await unitOfWork.Products.GetAsync(id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        return mapper.Map<ProductDTO>(product);
    }

    public async Task UpdateAsync(ProductDTO productDTO)
    {
        var dbEntry = await unitOfWork.Products.GetAsync(productDTO.Id);
        if (dbEntry == null)
            throw new KeyNotFoundException("Product not found for update");

        dbEntry.Name = productDTO.Name;
        dbEntry.Category = productDTO.Category;
        dbEntry.Description = productDTO.Description;
        dbEntry.Price = productDTO.Price;
        dbEntry.ImageName = productDTO.ImageName;

        unitOfWork.Products.Update(dbEntry);
        await unitOfWork.SaveAsync();
    }

    public async Task<ProductDTO> FindAsync(Guid id)
    {
        var product = await unitOfWork.Products.FindAsync(p => p.Id == id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        return mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> DeleteAsync(Guid id)
    {
        var product = await unitOfWork.Products.FindAsync(p => p.Id == id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        unitOfWork.Products.Delete(await unitOfWork.Products.GetAsync(id));
        await unitOfWork.SaveAsync();
        return mapper.Map<ProductDTO>(product);
    }

    public async Task<bool> CheckItemAsync(Guid idItem)
    {
        try
        {
            var product = await unitOfWork.Products.GetAsync(idItem);
            return product != null;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        unitOfWork.Dispose();
    }
}