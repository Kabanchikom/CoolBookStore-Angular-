using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Product.DTO;
using Web.Areas.Product.Models;
using Web.EF;

namespace Web.Areas.Product.Services;

public class ProductService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public ProductService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductCatalogItemsResponseDto> GetProductsAsync(ProductsCatalogItemsRequestDto catalogItemsRequest)
    {
        var products = _context
            .Products
            .Include(x => x.Manufacturer)
            .Include(x => (x as BookCard)!.Authors);

        IQueryable<ProductCard> filtered;

        if (catalogItemsRequest.Filter == null)
        {
            filtered = products;
        }
        else
        {
            filtered = products.Where(x =>
                    (x as BookCard)!.Authors
                        .Select(y => y.Initials)
                        .Except(catalogItemsRequest.Filter.Authors ?? new List<string>())
                        .Any() == false
                    && x.Discount != 0 || !catalogItemsRequest.Filter.IsDiscount
                    && x.NewPrice >= catalogItemsRequest.Filter.MinPrice
                    && x.NewPrice <= catalogItemsRequest.Filter.MaxPrice
                    
                    && ((x as BookCard)!.Genres & (BookGenre) (catalogItemsRequest.Filter.Genres ?? new List<string>())
                    .Select(y => (int) Enum.Parse<BookGenre>(y)).Sum()) != 0
                    && x.IsOnSale == catalogItemsRequest.Filter.IsOnSale
            );
        }

        var productsDto =
            _mapper.Map<List<ProductCard>,List<ProductCatalogItemsResponseDto.ProductItemData>>(
                    await filtered.ToListAsync());

        var paging = catalogItemsRequest.Paging;

        if (paging == null)
        {
            return new ProductCatalogItemsResponseDto
            {
                Products = productsDto,
                Total = productsDto.Count,
            };
        }

        return new ProductCatalogItemsResponseDto
        {
            Products = productsDto
                .Skip((paging.Page - 1) * paging.Limit)
                .Take(paging.Limit).ToList(),
            Total = productsDto.Count
        };
    }

    public async Task<ProductDetailDto?> GetProductAsync(Guid productId)
    {
        var product = await _context
            .Products
            .Include(x => x.Manufacturer)
            .Include(x => (x as BookCard)!.Authors)
            .FirstOrDefaultAsync(x => x.Id == productId);

        return product == null 
            ? null 
            : _mapper.Map<ProductCard, ProductDetailDto>(product);
    }

    public List<string> GetGenres() => 
        _mapper.Map<IEnumerable<BookGenre>, List<string>>(Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
}