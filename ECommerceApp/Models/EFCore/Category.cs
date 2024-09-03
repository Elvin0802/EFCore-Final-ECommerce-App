namespace ECommerceApp.Models.EFCore;

public class Category
{
	public int CategoryId { get; set; }
	public string Name { get; set; }
	public ICollection<Product> Products { get; set; }

	public Category() { }
	public Category(string categoryName)
	{
		Name = categoryName;
	}

	public override string ToString() => Name;
}