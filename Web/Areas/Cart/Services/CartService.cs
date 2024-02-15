using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Account.Models;
using Web.Areas.Cart.DTO;
using Web.Areas.Cart.Models;
using Web.Areas.Product.Models;
using Web.EF;

namespace Web.Areas.Cart.Services;

public class CartService
{
    private readonly BookStoreDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CartService(
        BookStoreDbContext context,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<CartLineResponseDto>> AddRangeAsync(Guid actorId, List<CartLineAddRequestDto> request)
    {
        var duplicates = request
            .GroupBy(x => x.ProductId)
            .Where(y => y.Skip(1).Any())
            .SelectMany(x => x);

        // Todo: прокинуть в эксепшен дублирующиеся позиции
        
        if (duplicates.Any())
        {
            throw new ArgumentException("Дублирующиеся позиции в корзине");
        }

        foreach (var cartLine in request)
        {
            await AddAsync(actorId, cartLine);
        }
        
        await _context.SaveChangesAsync();
        
        var cart = await GetCartAsync(actorId);

        return cart;
    }

    public async Task<Guid> AddAsync(Guid actorId, CartLineAddRequestDto request)
    {
        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Количество товара <= 0");
        }
        
        var owner = await _userManager.FindByIdAsync(request.OwnerId);
        
        if (owner == null)
        {
            throw new KeyNotFoundException($"Пользователь с id={request.OwnerId} не найден");
        }
        
        if (actorId.ToString() != owner.Id)
        {
            throw new UnauthorizedAccessException("У пользователя нет прав на добавление в чужую корзину");
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(
            x => x.Id.ToString() == request.ProductId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Продукт с id={request.ProductId} не найден");
        }
        
        if (product.Quantity <= 0)
        {
            throw new ArgumentException($"Товар с id={request.ProductId} закончился на складе");
        }
        
        var cartLine = await _context.CartLines.FirstOrDefaultAsync(x => 
            x.ProductId.ToString() == request.ProductId
            && x.OwnerId == request.OwnerId);

        if (cartLine != null)
        {
            cartLine.Quantity += request.Quantity;
        }
        else
        {
            cartLine = new CartLine
            {
                Product = product,
                Owner = owner,
                Quantity = request.Quantity,
            };
            await _context.CartLines.AddAsync(cartLine);
        }
        
        await _context.SaveChangesAsync();

        return cartLine.Id;
    }
    
    public async Task<List<CartLineResponseDto>> GetCartAsync(Guid actorId)
    {
        var cartLines = await _context
            .CartLines
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Include(x => x.Product)
            .ThenInclude(x => (x as BookCard)!.Authors)
            .Where(x => x.OwnerId == actorId.ToString())
            .ToListAsync();
        
        var result = _mapper.Map<List<CartLineResponseDto>>(cartLines);
        return result;
    }

    public async Task UpdateAsync(Guid id, Guid actorId, CartLineUpdateRequestDto request)
    {
        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Количество товара <= 0");
        }

        var cartLine = await _context
            .CartLines
            .FirstOrDefaultAsync(x => x.Id == id);

        var owner = await _userManager.FindByIdAsync(actorId.ToString());
        var product = await _context.Products.FirstOrDefaultAsync(
            x => x.Id.ToString() == request.ProductId);

        if (cartLine == null)
        {
            throw new KeyNotFoundException($"Элемент корзины с id={id} не найден");
        }
        
        if (product == null)
        {
            throw new KeyNotFoundException($"Продукт с id={request.ProductId} не найден");
        }

        cartLine.Owner = owner;
        cartLine.Product = product;
        cartLine.Quantity = request.Quantity;
        
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid productId, Guid actorId)
    {
        var cartLine = await _context
            .CartLines
            .FirstOrDefaultAsync(x =>
                x.ProductId == productId
                && x.OwnerId == actorId.ToString());
        
        if (cartLine == null)
        {
            throw new KeyNotFoundException($"Элемент корзины с id={productId} не найден");
        }
        
        if (cartLine.OwnerId != actorId.ToString())
        {
            throw new ArgumentException("У пользователя нет прав на удаление из чужой корзины");
        }
        
        _context.Remove(cartLine);
        await _context.SaveChangesAsync();
    }

    public async Task ClearAsync(Guid actorId)
    {
        var owner = await _userManager.FindByIdAsync(actorId.ToString());
        
        if (owner == null)
        {
            throw new KeyNotFoundException("Пользователь не найден");
        }
        
        var cartLines =  await _context
            .CartLines
            .Where(x => x.OwnerId == actorId.ToString())
            .ToListAsync();
        
        _context.RemoveRange(cartLines);
        await _context.SaveChangesAsync();
    }
}