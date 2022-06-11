using Microsoft.EntityFrameworkCore;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Data.Repository;
using NLayer.Data.UnitOfWork;
using NLayer.Service.DTOs;
using NLayer.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<ProductFeature> _productFeatureRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ProductDto>>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            var productDto = products.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            }).ToList();

            if (!productDto.Any())
            {
                return new Response<List<ProductDto>>()
                {
                    Data = null,
                    Errors = new List<string>() { "There is no product here!" },
                    Status = 404
                };
            }

            return new Response<List<ProductDto>>()
            {
                Data = productDto,
                Errors = null,
                Status = 200
            };
        }
        public async Task<Response<string>> CreateAll(Category category, Product product, ProductFeature productFeature)
        {
            await _productRepository.Add(product);
            await _categoryRepository.Add(category);
            await _productFeatureRepository.Add(productFeature);


            await _unitOfWork.Commit();
            return new Response<string>();
        }
    }
}
