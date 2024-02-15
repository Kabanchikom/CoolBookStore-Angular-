using Web.Areas.Account.Models;
using Web.Areas.Product.Models;
using Web.Models;

namespace Web.Areas.Cart.Models;

/// <summary>
/// Позиция в корзине.
/// </summary>
public class CartLine : Entity<Guid>
{
    public ProductCard Product { get; set; }
    
    public Guid ProductId { get; set; }
    
    public User Owner { get; set; }
    
    public string OwnerId { get; set; }
    
    public int Quantity { get; set; }
}