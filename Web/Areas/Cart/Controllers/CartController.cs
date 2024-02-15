using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Account.Services;
using Web.Areas.Cart.DTO;
using Web.Areas.Cart.Services;

namespace Web.Areas.Cart.Controllers;

[Route("api/cart")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpPost("add")]
    public async Task<ActionResult<Guid>> Add([FromBody] CartLineAddRequestDto request)
    {
        try
        {
            var actorId = AccountService.ExtractUserId(HttpContext);
            var cartLineId = await _cartService.AddAsync(actorId, request);
            return cartLineId;
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpPost("addRange")]
    public async Task<ActionResult<List<CartLineResponseDto>>> AddRange([FromBody] List<CartLineAddRequestDto> request)
    {
        try
        {
            var actorId = AccountService.ExtractUserId(HttpContext);
            var cartLines = await _cartService.AddRangeAsync(actorId, request);
            return cartLines;
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("getCart")]
    public async Task<ActionResult<List<CartLineResponseDto>>> GetCart()
    {
        var actorId = AccountService.ExtractUserId(HttpContext);
        return await _cartService.GetCartAsync(actorId);
    }

    [HttpGet("getCount")]
    public async Task<ActionResult<int>> GetCount()
    {
        var actorId = AccountService.ExtractUserId(HttpContext);
        return (await _cartService.GetCartAsync(actorId)).Sum(x => x.Quantity);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] CartLineUpdateRequestDto request)
    {
        try
        {
            var ownerId = AccountService.ExtractUserId(HttpContext);
            await _cartService.UpdateAsync(id, ownerId, request);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }
    
    [HttpDelete("delete/{productId}")]
    public async Task<ActionResult> Delete(Guid productId)
    {
        try
        {
            var actorId = AccountService.ExtractUserId(HttpContext);
            await _cartService.DeleteAsync(productId, actorId);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpDelete("clear")]
    public async Task<ActionResult> Clear()
    {
        try
        {
            var actorId = AccountService.ExtractUserId(HttpContext);
            await _cartService.ClearAsync(actorId);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}