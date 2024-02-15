using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Areas.Manufacturer.Models;

namespace Web.Areas.Product.DTO;

public class ProductDetailDto
{
    public string Id { get; set; }
    
    [Display(Name = "Название")]
    public string Name { get; set; }
    
    [Display(Name = "Фото товара")]
    public string? ImgSrc { get; set; }

    [Display(Name = "Цена со скидкой")]
    [Range(1, double.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public decimal? NewPrice { get; set; }
    
    [Display(Name = "Цена без скидки")]
    [Range(1, double.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public decimal OldPrice { get; set; }

    [Display(Name = "Короткое описание")]
    public string? ShortDescription { get; set; }
    
    [Display(Name = "Длинное описание")]
    public string? LongDescription { get; set; }
    
    /// <summary>
    /// Производитель.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Оценка.
    /// </summary>
    /// <remarks>Сколько звезд.</remarks>
    [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Display(Name = "Оценка")]
    public decimal Rating { get; set; }

    /// <summary>
    /// Участвует ли в распродаже.
    /// </summary>
    [Display(Name = "Распродажа")]
    public bool IsOnSale { get; set; }
}