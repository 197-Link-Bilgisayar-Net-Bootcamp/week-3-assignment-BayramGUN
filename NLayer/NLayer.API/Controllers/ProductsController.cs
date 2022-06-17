using Microsoft.AspNetCore.Mvc;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service;
using NLayer.Service.DTOs;
using NLayer.Service.Models;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
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
        /// <summary>
        ///  Tümünü alır gibisine :)
        /// </summary>
        /// <param name="product"></param>
        /// <param name="category"></param>
        /// <param name="productFeature"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(AllDto allDto)
        {
            var response = await _productService.CreateAll(allDto);
            return new ObjectResult(response) { StatusCode = response.Status };
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var idS = Convert.ToInt32(id);
            var response = await _productService.DeleteData(id);
            return new ObjectResult(response) { StatusCode = response.Status };
        }
       
    }
}
