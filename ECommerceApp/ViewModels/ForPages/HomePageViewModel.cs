using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class HomePageViewModel : BaseViewModel
{
	private User upriv;
	public User u1 { get=>upriv; set { upriv=value; OnPropertyChanged(); } }

	public HomePageViewModel()
	{
		AddCommand = new RelayCommand<object>(AddCommandExecute);


		Category c = new Category() { Name="TestCategory" };

		var db = App.Container!.GetInstance<AppDbContext>();

		db.Categories.Add(c);
		Cart cart1 = new Cart() { CartItems=new List<CartItem>() };

		u1 = new()
		{
			Username="user1",
			Address="i m e34",
			Cart=cart1,
			City="Baku",
			Country="Az",
			Email="elvincode1517@gmail.com",
			FirstName="Elvin",
			LastName="Siracli",
			Orders = new List<Order>(),
			PasswordHash="elvin123",
			PhoneNumber="+994515276567"
   ,
			PostalCode="Az-az",
			State="RandomState"
		};

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

		ProductReview pr1 = new ProductReview()
		{
			Rating=5,
			Review="Good",
			User =u1,
			DateCreated=DateTime.Now,
		};

		ProductReview pr2 = new ProductReview()
		{
			Rating=3,
			Review="Normal",
			User =u1,
			DateCreated=DateTime.Now,
		};

		ProductReview pr3 = new ProductReview()
		{
			Rating=1,
			Review="Bad",
			User =u1,
			DateCreated=DateTime.Now,
		};

		db.Products.Add(p1);
		db.Products.Add(p2);

		db.ProductReviews.Add(pr1);
		db.ProductReviews.Add(pr2);
		db.ProductReviews.Add(pr3);

		p1.ProductReviews.Add(pr1);
		p2.ProductReviews.Add(pr2);
		p2.ProductReviews.Add(pr3);

		db.SaveChanges();

		Ps = [p1, p2];

	}

	private int sortIndex;

	public int SortIndex
	{
		get => sortIndex;
		set
		{
			try
			{
				sortIndex=value;
				OnPropertyChanged();
				SortProducts(value);
			}
			catch { MessageBox.Show("Error in Sorting."); }
		}
	}

	private List<Product> ps;
	public List<Product> Ps { get=>ps; set { ps=value;OnPropertyChanged(); } }


	#region List Sort Function

	public void SortProducts(int index)
	{
		try
		{
			if (index == 0)
				Ps = new(Ps?.OrderBy(p => p.Name)!);
			else if (index == 1)
				Ps = new(Ps?.OrderBy(p => p.Price)!);
			else if (index == 2)
				Ps = new(Ps?.OrderByDescending(p => p.Name)!);
			else if (index == 3)
				Ps = new(Ps?.OrderByDescending(p => p.Price)!);

		//	App.Container!.GetInstance<HomePageView>().ProductsView.ItemsSource = Ps;
			App.Container!.GetInstance<HomePageView>().ProductsView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Exams not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion


	public ICommand AddCommand { get; set; }

	public void AddCommandExecute(object? obj)
	{

	if((obj as Product) is not null)
		MessageBox.Show(obj.ToString()+ "  Not Null.","Product.");
	else
		MessageBox.Show("is Null.","Product.");

		return;


		var items = App.Container!
			.GetInstance<CartPageViewModel>()
			.Cart!
			.CartItems.ToList();

		var product = (App.Container!
					.GetInstance<HomePageView>()
					.ProductsView.SelectedItem as Product);

		var ci = items.Find(ci => ci.ProductId == product!.ProductId);

		if (ci is not null)
		{
			App.Container!
			.GetInstance<CartPageViewModel>()
			.Cart!
			.CartItems.FirstOrDefault(ci).Quantity++;
		}
		else
		{
			App.Container!
			.GetInstance<CartPageViewModel>()
			.Cart!
			.CartItems
			.Add(new CartItem()
			{
				Product = product!,
				Quantity = 1
			});
		}

		MessageBox.Show("Product Added.","Adding.");


		App.Container!.GetInstance<CartPageView>().CartView.Items.Refresh();

	}
}
