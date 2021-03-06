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
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(AppDbContext context, 
                IGenericRepository<Product> productRepository, 
                IUnitOfWork unitOfWork)
        {
            _context = context;
            _productRepository = productRepository;
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

        public Task CreateAll(AllDto allDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProductDto>> GetById(int id)
        {
            var product = await _context.Products.Where(p => p.Id == id).SingleOrDefaultAsync();
            
            if (product is null)
            {
                return new Response<ProductDto>()
                {
                    Data = null,
                    Errors = new List<string>() { "There is no product with this id!" },
                    Status = 404
                };
            }
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return new Response<ProductDto>()
            {
                Data = productDto,
                Errors = null,
                Status = 200
            };
        }
        public async Task<Response<string>> DeleteData(int id)
        {
            await _productRepository.Delete(id);
            await _unitOfWork.Commit();
            
            var response = new Response<string>()
            {
                Status = 200,
                Errors = null
            };
            return response;
        }
    }
}
