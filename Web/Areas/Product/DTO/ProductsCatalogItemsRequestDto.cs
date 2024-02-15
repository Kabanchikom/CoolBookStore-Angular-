using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Web.Areas.Product.DTO;

public class ProductsCatalogItemsRequestDto
{
    public class PagingData
    {
        /// <summary>
        /// Номер страницы.
        /// </summary>]
        public int Page { get; set; }

        /// <summary>
        /// Количество элементов на странице.
        /// </summary>
        public int Limit { get; set; }
    }

    public PagingData? Paging { get; set; }

    public class FilterData
    {
        public List<string>? Genres { get; set; }
        public List<string>? Authors { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsDiscount { get; set; }
    }
    
    public FilterData? Filter { get; set; }
}