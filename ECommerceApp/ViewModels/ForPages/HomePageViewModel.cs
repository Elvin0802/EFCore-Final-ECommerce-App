using ECommerceApp.Models;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;

namespace ECommerceApp.ViewModels.ForPages;

public class HomePageViewModel : BaseViewModel
{
    public HomePageViewModel()
    {
        Category c = new Category() { Name="TestCategory" };

		var db = App.Container!.GetInstance<AppDbContext>();

		db.Categories.Add(c);

        Product p1 = new Product()
		{
			Name="testProduct 1111",
			Description="Test of app1",
			Category=c,
			CategoryId=1,
			DateAdded=DateTime.Now,
			IsActive=true,
			Price=12
		,
			ProductImages = new List<ProductImage>(),
			StockQuantity=1000,
			ProductReviews = new List<ProductReview>()
		};
        Product p2 = new Product()
		{
			Name="testProduct 2222",
			Description="Test of app2",
			Category=c,
			CategoryId=1,
			DateAdded=DateTime.Now,
			IsActive=true,
			Price=54
		,
			ProductImages = new List<ProductImage>(),
			StockQuantity=500,
			ProductReviews = new List<ProductReview>()
		};

		db.Products.Add(p1);
		db.Products.Add(p2);

		db.SaveChanges();

        Ps = [p1,p2];

    }

    public List<Product> Ps { get; set; }
}
