namespace ECommerceApp.Models.EFCore;

public class Cart
{
	public int CartId { get; set; }
	public int UserId { get; set; }
	public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

	public User User { get; set; }
	public ICollection<CartItem> CartItems { get; set; }
}