namespace ECommerceApp.Models.EFCore;

public class User
{
	public int UserId { get; set; }

	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public bool Role { get; set; } // 1 = Admin , 0 = Customer

	// Profile Information
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }

	// Relationships
	public ICollection<Order> Orders { get; set; }
	public Cart Cart { get; set; }
}