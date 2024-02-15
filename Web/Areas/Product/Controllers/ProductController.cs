using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Product.DTO;
using Web.Areas.Product.Services;

namespace Web.Areas.Product.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
        
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
        
    [AllowAnonymous]
    [HttpPost("catalogItems")]
    public async Task<ActionResult<ProductCatalogItemsResponseDto>> GetCatalogItems(
        [FromBody] ProductsCatalogItemsRequestDto catalogItemsRequest
    )
    {
        return await _productService.GetProductsAsync(catalogItemsRequest);
    }
    
    [AllowAnonymous]
    [HttpGet("detail/{id:Guid}")]
    public async Task<ActionResult<ProductDetailDto>> GetCatalogItemDetail(Guid id)
    {
        var result = await _productService.GetProductAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
}