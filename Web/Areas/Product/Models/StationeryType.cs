using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Product.Models;

/// <summary>
/// Канцелярские товары
/// </summary>
public enum StationeryType
{
    [Display(Name = "Ручки")] 
    Pen,
        
    [Display(Name = "Карандаши")] 
    Pencil,
        
    [Display(Name = "Бумага")] 
    Paper,
        
    [Display(Name = "Кисти")] 
    Brush,
        
    [Display(Name = "Тетради")] 
    Notebook,
}