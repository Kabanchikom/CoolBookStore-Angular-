using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Product.DTO;
using Web.Areas.Product.Models;

namespace Web.Areas.Genres;

[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly IMapper _mapper;
    
    public GenresController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public List<GenresResponseDto> GetGenres() => 
        _mapper.Map<IEnumerable<BookGenre>, List<GenresResponseDto>>(
            Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
}