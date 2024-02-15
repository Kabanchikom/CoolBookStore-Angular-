using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Web.Areas.Cart.Models;
using Web.Models;

namespace Web.Areas.Order.Models;

public class OrderCard : Entity<Guid>
{
    /// <summary>
    /// Имя получателя.
    /// </summary>
    [Required]
    [DisplayName("Имя")]
    public string Name { get; set; }

    /// <summary>
    /// Отчество получателя.
    /// </summary>
    [DisplayName("Отчество")]
    public string Patronimic { get; set; }

    /// <summary>
    /// Фамилия получателя.
    /// </summary>
    [Required]
    [DisplayName("Фамилия")]
    public string Surname { get; set; }

    /// <summary>
    /// Id статуса заказа.
    /// </summary>
    [DisplayName("Статус заказа")]
    public int StatusId { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    [Display(Name = "Статус")]
    public OrderStatus Status { get; set; } = OrderStatus.New;

    /// <summary>
    /// Способ доставки.
    /// </summary>
    [DisplayName("Способ доставки")]
    public DeliveryType DeliveryType { get; set; }

    /// <summary>
    /// Адрес доставки.
    /// </summary>
    [Required]
    [DisplayName("Адрес доставки")]
    public string Address { get; set; }

    /// <summary>
    /// Дата заказа.
    /// </summary>
    [DisplayName("Дата заказа")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Позиции в корзине.
    /// </summary>
    [JsonIgnore]
    [DisplayName("Товары")]
    public List<CartLine> CartLines { get; set; }
}