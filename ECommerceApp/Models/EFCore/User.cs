using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models.EFCore;

public class User
{
	public int UserId { get; set; }

	// Basic User Info
	[Required, MaxLength(50)]
	public string Username { get; set; }
	[Required, MaxLength(100)]
	public string Email { get; set; }
	[Required]
	public string PasswordHash { get; set; }
	public string Role { get; set; } = "Customer";
	public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	public bool IsActive { get; set; } = true;

	// Profile Information
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string PostalCode { get; set; }
	public string Country { get; set; }

	// Relationships
	public ICollection<Order> Orders { get; set; }
	public Cart Cart { get; set; }
	public ICollection<ProductReview> ProductReviews { get; set; } // Add this line
}