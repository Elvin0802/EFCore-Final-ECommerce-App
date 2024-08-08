using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models.EFCore;


public class ProductReview
{
	public int ProductReviewId { get; set; }
	public int ProductId { get; set; }
	public int UserId { get; set; }
	[Range(1, 5)]
	public int Rating { get; set; }
	public string Review { get; set; }
	public DateTime DateCreated { get; set; } = DateTime.UtcNow;

	public Product Product { get; set; }
	public User User { get; set; }
}