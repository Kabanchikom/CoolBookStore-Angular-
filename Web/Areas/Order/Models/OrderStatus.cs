using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Order.Models;

public enum OrderStatus
{
    [Display(Name = "Новый")]
    New = 0,

    [Display(Name = "Отсроченный")]
    Deferred = 1,

    [Display(Name = "Готов к отправке")]
    ReadyForShipment = 2,

    [Display(Name = "Отправлен")]
    Shipped = 3,

    [Display(Name = "Доставлен")]
    Delivered = 4,

    [Display(Name = "Проблема с доставкой")]
    NotDelivered = 5,

    [Display(Name = "Выполнен")]
    Completed = 6,

    [Display(Name = "Отменен")]
    Canceled = 7,

    [Display(Name = "На утверждении")]
    Approving = 8,
        
    [Display(Name = "Предзаказ")]
    Preorder = 9,
        
    [Display(Name = "Возвращен")]
    Returned = 10,
        
    [Display(Name = "Резерв")]
    Reserved = 11,

    [Display(Name = "Не дозвон")]
    NotDialing = 14,
        
    [Display(Name = "Рассрочка")]
    Credit = 15,
        
    [Display(Name = "Возврат ДС")]
    ReturnMoney = 16
}