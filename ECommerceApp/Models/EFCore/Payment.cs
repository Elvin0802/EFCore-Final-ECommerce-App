namespace ECommerceApp.Models.EFCore;

public class Payment
{
	public int PaymentId { get; set; }
	public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
	public decimal PaymentAmount { get; set; }
	public string PaymentStatus { get; set; }

	public int OrderId { get; set; }
	public Order Order { get; set; }
}
