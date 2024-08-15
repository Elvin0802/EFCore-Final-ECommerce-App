namespace ECommerceApp.Models.EFCore;

public class Category
{
	public int CategoryId { get; set; }
	public string Name { get; set; }
	public ICollection<Product> Products { get; set; }

	public override string ToString() => Name;
	public string GetAd() => "GetAd Isledi.";
}