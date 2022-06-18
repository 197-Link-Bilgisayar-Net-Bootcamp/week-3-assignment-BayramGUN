using Microsoft.AspNetCore.Mvc;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service;
using NLayer.Service.DTOs;
using NLayer.Service.Models;

namespace NLayer.API.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class GenericController : Controller
{
private readonly GenericService _productService;

    public GenericController(GenericService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AllDto allDto)
    {
            var response = await _productService.CreateAll(allDto);
            return new ObjectResult(response) { StatusCode = response.Status };
    }
}