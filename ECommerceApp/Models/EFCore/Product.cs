using ECommerceApp.Services.Additional;

namespace ECommerceApp.Models.EFCore;

public class Product : NotifyService
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int StockQuantity { get; set; }
	public int CategoryId { get; set; }
	public DateTime DateAdded { get; set; } = DateTime.UtcNow;
	public string MainImage { get => ProductImages.FirstOrDefault()!.ImageUrl; }

	public Category Category { get; set; }
	public ICollection<ProductImage> ProductImages { get; set; }

}