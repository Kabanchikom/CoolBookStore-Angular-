using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Web.Areas.Product.DTO;

public class ProductCatalogItemsResponseDto
{
    public class ProductItemData
    {
        public string Id { get; set; }

        [Display(Name = "Название")] public string Name { get; set; }

        [Display(Name = "Изображение в каталоге")]
        public string? ImgSrc { get; set; }

        [Display(Name = "Цена до скидки")] public decimal OldPrice { get; set; }

        [Display(Name = "Цена со скидкой")] public decimal? NewPrice { get; set; }

        // /// <summary>
        // /// Производитель.
        // /// </summary>
        // [Display(Name = "Производитель")]
        // public string? Manufacturer { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        /// <remarks>Сколько звезд.</remarks>
        [Display(Name = "Оценка")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Участвует ли в распродаже.
        /// </summary>
        [Display(Name = "Распродажа")]
        public bool IsOnSale { get; set; }

        /// <summary>
        /// Описание товара в каталоге.
        /// </summary>
        public string Description { get; set; }
    }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [Display(Name = "Продукты")] 
    public List<ProductItemData>? Products { get; set; }
    
    [Display(Name = "Всего продуктов")]
    [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Total { get; set; }
}