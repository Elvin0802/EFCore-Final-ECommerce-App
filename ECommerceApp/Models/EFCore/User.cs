using System.Windows.Controls;

namespace ECommerceApp.Models.EFCore;

public class User
{
	public int UserId { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	//public ICollection<Order> Orders { get; set; }
}