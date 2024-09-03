namespace ECommerceApp.Models.EFCore;

public class ProductImage
{
	public int ProductImageId { get; set; }
	public int ProductId { get; set; }
	public string ImageUrl { get; set; }

	public Product Product { get; set; }
}