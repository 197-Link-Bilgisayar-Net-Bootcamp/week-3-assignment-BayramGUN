using Microsoft.AspNetCore.Mvc;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service;
using NLayer.Service.DTOs;
using NLayer.Service.Models;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();
            return new ObjectResult(response) { StatusCode = response.Status };
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllData(CategoryDto newCategoryEntity, ProductDto newProductEntity, ProductFeatureDto newProductFeatureDto)
        {
            var response = await _productService.CreateAll(newCategoryEntity, newProductEntity, newProductFeatureDto);
            return new ObjectResult(response) { StatusCode = response.Status };
        }
       
    }
}
