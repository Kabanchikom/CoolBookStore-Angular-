using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Order.Models;

public enum DeliveryType
{
    [Display(Name = "Самовывоз")]
    PickUp = 0,

    [Display(Name = "Курьер")]
    Courier = 1,
}