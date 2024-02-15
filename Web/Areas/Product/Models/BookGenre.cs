using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Product.Models;

[Flags]
public enum BookGenre
{
    [Display(Name = "Фантастика")]
    Fiction = 1 << 0,
    
    [Display(Name = "Проза")]
    Prose = 1 << 1,
    
    [Display(Name = "Классика")]
    Classics = 1 << 2,
    
    [Display(Name = "Философия")]
    Philosophy = 1 << 3,
    
    [Display(Name = "Ужасы")]
    Horror = 1 << 4,
    
    [Display(Name = "Поэзия")]
    Poetry = 1 << 5,
    
    [Display(Name = "Роман")]
    Novel = 1 << 6,
    
    [Display(Name = "Образование")]
    Education = 1 << 7,
}