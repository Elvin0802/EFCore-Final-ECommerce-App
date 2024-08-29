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
	public int Fv { get => _fv; set { _fv=value; OnPropertyChanged(); SortProducts(4); } } // Value of First Slider.
	public int Sv { get => _sv; set { _sv=value; OnPropertyChanged(); SortProducts(4); } } // Value of Second Slider.

	public AppDbContext Db { get => App.Container!.GetInstance<AppDbContext>(); } // Main DB Context of App.

	public HomePageViewModel()
	{
		AddCommand = new RelayCommand<object>(AddCommandExecute);
		ShowCommand = new RelayCommand<object>(ShowCommandExecute);
		Adm = new RelayCommand<object>(AdmExec);

		Db.Products.ElementAt(0).Category = Db.Categories.FirstOrDefault()!;
		Db.Products.ElementAt(1).Category = Db.Categories.FirstOrDefault()!;
		Db.Products.ElementAt(2).Category = Db.Categories.FirstOrDefault()!;
		Db.Products.ElementAt(3).Category = Db.Categories.FirstOrDefault()!;

		u1 = Db.Users.FirstOrDefault()!;
		u1.Cart = Db.Carts.FirstOrDefault()!;
		u1.Cart.CartItems = new List<CartItem>();

		Db.Products.ElementAt(0).ProductReviews = [Db.ProductReviews.ElementAt(0), Db.ProductReviews.ElementAt(1)];
		Db.Products.ElementAt(1).ProductReviews = [Db.ProductReviews.ElementAt(0), Db.ProductReviews.ElementAt(1)];
		Db.Products.ElementAt(2).ProductReviews = [Db.ProductReviews.ElementAt(0), Db.ProductReviews.ElementAt(1)];
		Db.Products.ElementAt(3).ProductReviews = [
			new ProductReview()
			{  ProductId = 4 , Rating = 5 , Review = "Upper.", User = Db.Users.FirstOrDefault(), UserId=1, DateCreated = DateTime.Now}
		];

		Fv = Min;
		Sv = Max;

		Ps = Db.Products.ToList();

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
			//Ps = new(Ps?.OrderBy(p => p.Name)!); // 0
			//Ps = new(Ps?.OrderBy(p => p.Price)!); // 1

			if (index == 0)
				Ps = Db.Products.OrderBy(p => p.Name).ToList();
			else if (index == 1)
				Ps = Db.Products.OrderBy(p => p.Price).ToList();
			else if (index == 2)
				Ps = Db.Products.OrderByDescending(p => p.Name).ToList();
			else if (index == 3)
				Ps = Db.Products.OrderByDescending(p => p.Price).ToList();
			else if (index == 4) // price sorting
				Ps = Db.Products.Where(product => product.Price >= Fv && product.Price <= Sv).ToList();

			//	App.Container!.GetInstance<HomePageView>().ProductsView.ItemsSource = Ps;
			App.Container!.GetInstance<HomePageView>().ProductsView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Products not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion


	//	public int GetMax() => (int)App.Container!.GetInstance<AppDbContext>().Products.Min(p => p.Price);
	public int GetMax()
	{

		var db = App.Container!.GetInstance<AppDbContext>();
		db.Products.Load();
		var t = db.Products.ToList();

		return ((int)t.Max(p => p.Price))+1;

	}

	public ICommand Adm { get; set; }
	public ICommand AddCommand { get; set; }

	public void AdmExec(object? obj)
	{

		var p = App.Container!.GetInstance<AdminPageView>();
		p.DataContext = App.Container.GetInstance<AdminPageViewModel>();

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}
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
