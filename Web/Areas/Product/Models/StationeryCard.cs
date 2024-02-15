using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Areas.Product.Models;

/// <summary>
/// Канцелярский товар
/// </summary>
public class StationeryCard : ProductCard
{
    /// <summary>
    /// Тип канцелярского товара.
    /// </summary>
    [Display(Name = "Тип канцелярского товара")]
    public StationeryType StationeryType { get; set; }
}