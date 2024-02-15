using AutoMapper;
using Web.Areas.Account.DTO;
using Web.Areas.Account.Models;
using Web.Areas.Cart.DTO;
using Web.Areas.Cart.Models;
using Web.Areas.Manufacturer.Models;
using Web.Areas.Product.DTO;
using Web.Areas.Product.Models;
using Web.Helpers;

namespace Web.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        #region ProductCard => ProductsResponseDTO

        CreateMap<ProductCard, ProductCatalogItemsResponseDto.ProductItemData>()
            .IncludeAllDerived()
            .ForMember(dest => dest.ImgSrc,
                opt =>
                    opt.MapFrom(src => src.ThumbImgSrc));

        CreateMap<BookCard, ProductCatalogItemsResponseDto.ProductItemData>()
            .ForMember(dest => dest.Description,
                opt =>
                    opt.MapFrom(src =>
                        string.Join(", ", src.Authors.Select(x => x.Initials).ToArray())));

        CreateMap<StationeryCard, ProductCatalogItemsResponseDto.ProductItemData>()
            .ForMember(dest => dest.Description,
                opt =>
                    opt.MapFrom(src => src.StationeryType.GetDisplayName()));

        CreateMap<ManufacturerCard, string>().ConvertUsing(x => x.Name);
        CreateMap<BookGenre, List<string?>>().ConvertUsing(x => x.GetDisplayNames());

        #endregion

        #region ProductCard => ProductDetailDTO

        CreateMap<ProductCard, ProductDetailDto>()
            .IncludeAllDerived();

        CreateMap<BookCard, ProductDetailDto>()
            .ForMember(dest => dest.ShortDescription,
                opt =>
                    opt.MapFrom(src =>
                        string.Join(", ", src.Authors.Select(x => x.Initials).ToArray())));

        CreateMap<StationeryCard, ProductDetailDto>()
            .ForMember(dest => dest.ShortDescription,
                opt =>
                    opt.MapFrom(src => src.StationeryType.GetDisplayName()));

        #endregion

        #region User => UserDto

        CreateMap<User, UserDto>();

        #endregion

        // BookGenre => GenresResponseDTO

        CreateMap<BookGenre, GenresResponseDto>()
            .ConvertUsing(x => new GenresResponseDto
        {
            Value = x.ToString(),
            Description = x.GetDisplayName()
        });
        
        // CreateMap<List<string>, BookGenre>().ConvertUsing(x => x.Select(x =>
        // {
        //     BookGenre value;
        //     Enum.TryParse(x, out value);
        // }))
        
        CreateMap<RegisterRequestDto, User>();

        #region Cart

        CreateMap<CartLineResponseDto, CartLine>();
        CreateMap<CartLine, CartLineResponseDto>()
            .ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ImgSrc,
                opt =>
                    opt.MapFrom(src => src.Product.ImgSrc))
            .ForMember(dest => dest.Description,
                opt =>
                    opt.MapFrom(src => src.Product.ShortDescription))
            .ForMember(dest => dest.OldPrice,
                opt =>
                    opt.MapFrom(src => src.Product.OldPrice))
            .ForMember(dest => dest.NewPrice,
                opt =>
                    opt.MapFrom(src => src.Product.NewPrice));

        #endregion
    }
}