using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.ViewModels.ForWindows;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class HomePageViewModel : BaseViewModel
{
	private User upriv;
	public User u1 { get => upriv; set { upriv=value; OnPropertyChanged(); } }

	private int _fv;
	private int _sv;

	public int Min { get => 0; set { OnPropertyChanged(); } } // Min Value of Product Price.
	public int Max { get => GetMax(); set { OnPropertyChanged(); } } // Max Value of Product Price.
	public int Avg { get => Max/2; set { OnPropertyChanged(); } } // Avg Value of Product Price.
	public int Fv { get => _fv; set { _fv=value; OnPropertyChanged(); } } // Value of First Slider.
	public int Sv { get => _sv; set { _sv=value; OnPropertyChanged(); } } // Value of Second Slider.

	public HomePageViewModel()
	{
		AddCommand = new RelayCommand<object>(AddCommandExecute);
		ShowCommand = new RelayCommand<object>(ShowCommandExecute);

		Fv = Min;
		Sv = Max;

		Category c = new Category() { Name="TestCategory" };

		var db = App.Container!.GetInstance<AppDbContext>();

		db.Categories.Add(c);
		Cart cart1 = new Cart() { CartItems=new List<CartItem>() };



		ProductImage pii1 = new()
		{
			ImageUrl = @"D:\Games\bmw1.jpg",
			IsMainImage = true
		};

		ProductImage pii2 = new()
		{
			ImageUrl = @"D:\Games\bmw2.jpg",
			IsMainImage = false
		};

		ProductImage pii3 = new()
		{
			ImageUrl = @"D:\Games\bmw3.jpg",
			IsMainImage = false
		};

		ProductImage pii4 = new()
		{
			ImageUrl = @"D:\Games\bmw5.jpg",
			IsMainImage = false
		};

		ProductImage pii5 = new()
		{
			ImageUrl = @"D:\Games\bmw8.jpg",
			IsMainImage = false
		};

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
			ProductImages = new List<ProductImage>() { pii1, pii2, pii3, pii4, pii5 },
			StockQuantity=90,
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
			StockQuantity=85,
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

		db.ProductImages.AddRange(pii1, pii2, pii3, pii4, pii5);

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
	public List<Product> Ps { get => ps; set { ps=value; OnPropertyChanged(); } }


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


	//	public int GetMax() => (int)App.Container!.GetInstance<AppDbContext>().Products.Min(p => p.Price);
	public int GetMax()
	{

		var db = App.Container!.GetInstance<AppDbContext>();
		db.Products.Load();
		var t = db.Products.ToList();

		return 100;
		//	return (int)t.Min(p => p.Price); 

	}

	public ICommand AddCommand { get; set; }

	public void AddCommandExecute(object? obj)
	{
		Product? SelectedProduct = obj as Product;

		var ci = u1.Cart.CartItems.ToList().Find(c => c.Product.ProductId == SelectedProduct!.ProductId);

		if (ci is not null)
		{
			ci.Quantity++;
			//u1.Cart.CartItems.FirstOrDefault(ci).Quantity++;
		}
		else
		{
			u1.Cart.CartItems
			.Add(new CartItem()
			{
				Product = SelectedProduct!,
				Quantity = 1
			});
		}

		MessageBox.Show("Product Added.", "Adding.");

		App.Container!.GetInstance<CartPageView>().CartView.Items.Refresh();

	}

	public ICommand ShowCommand { get; set; }

	public void ShowCommandExecute(object? obj)
	{
		try
		{
			Product? SelectedProduct = obj as Product;

			var win = new ProductDetailsWindowView();

			var dc = App.Container!.GetInstance<ProductDetailsWindowViewModel>();

			dc.Product = SelectedProduct!;

			win.DataContext = dc;

			win.ShowDialog();
		}
		catch
		{
			MessageBox.Show("Error in Show Command.");
		}


	}
}
