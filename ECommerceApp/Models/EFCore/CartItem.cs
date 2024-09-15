namespace ECommerceApp.Models.EFCore;

public class CartItem
{
	public int CartItemId { get; set; }
	public int Quantity { get; set; }
	public decimal TotalPrice { get => (Product.Price * Quantity); }


	public int CartId { get; set; }
	public Cart Cart { get; set; }


	public int ProductId { get; set; }
	public Product Product { get; set; }
}