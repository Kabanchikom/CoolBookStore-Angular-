namespace Web.Areas.Cart.DTO;

public class CartLineResponseDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string? ImgSrc { get; set; }
    
    public decimal OldPrice { get; set; }
    
    public decimal? NewPrice { get; set; }
    public string? Description { get; set; }
    public string ProductId { get; set; }
    public string OwnerId { get; set; }
    
    public int Quantity { get; set; }
}