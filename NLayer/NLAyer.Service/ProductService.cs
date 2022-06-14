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

        public ProductService(AppDbContext context,IGenericRepository<Category> categoryRepository, IGenericRepository<Product> productRepository, IGenericRepository<ProductFeature> productFeatureRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productFeatureRepository = productFeatureRepository;
            _unitOfWork = unitOfWork;
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
        public async Task<Response<string>> CreateAll(AllDto allDto)
        {
            var category = new Category()
            {
                Name = allDto.CategoryName,
            };
            var product = new Product()
            {
                Name = allDto.ProductName,
                Price = allDto.Price,
            };
            var productFeature = new ProductFeature()
            {
                Height = allDto.Height,
                Width = allDto.Width,
            };

            category.Products.Add(product);
            product.ProductFeature = productFeature;
               
            await _categoryRepository.Add(category);
            await _unitOfWork.Commit();
            return new Response<string>();
        }
    }
}
