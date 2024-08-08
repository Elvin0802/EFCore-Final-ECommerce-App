using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models.EFCore;

public class Product
{
	public int ProductId { get; set; }
	[Required, MaxLength(100)]
	public string Name { get; set; }
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
	public int StockQuantity { get; set; }
	public int CategoryId { get; set; }
	public DateTime DateAdded { get; set; } = DateTime.UtcNow;
	public bool IsActive { get; set; } = true;

	public Category Category { get; set; }
	public ICollection<ProductImage> ProductImages { get; set; }
	public ICollection<ProductReview> ProductReviews { get; set; }
}