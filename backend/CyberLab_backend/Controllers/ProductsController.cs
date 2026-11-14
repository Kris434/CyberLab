using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CyberLab_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Search(
        [FromQuery] string search = "")
    {
        var products = await _productService.SearchAsync(search);

        return Ok(products);
    }
}