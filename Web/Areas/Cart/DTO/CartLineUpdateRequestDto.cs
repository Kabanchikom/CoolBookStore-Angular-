namespace Web.Areas.Cart.DTO;

public class CartLineUpdateRequestDto
{
    public string ProductId { get; set; }
    
    //public string OwnerId { get; set; }
    
    public int Quantity { get; set; }
}