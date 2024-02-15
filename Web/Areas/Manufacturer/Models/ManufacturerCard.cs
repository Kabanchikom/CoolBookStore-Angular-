using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Web.Areas.Product.Models;
using Web.Models;

namespace Web.Areas.Manufacturer.Models;

/// <summary>
/// Производитель.
/// </summary>
public class ManufacturerCard : Entity<Guid>
{
    [Display(Name = "Название")] 
    public string Name { get; set; }
}