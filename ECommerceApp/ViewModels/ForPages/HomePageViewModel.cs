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
	private string _searchText;

	public int Min { get => 0; set { OnPropertyChanged(); } } // Min Value of Product Price.
	public int Max { get => GetMax(); set { OnPropertyChanged(); } } // Max Value of Product Price.
	public int Avg { get => Max/2; set { OnPropertyChanged(); } } // Avg Value of Product Price.
	public int Fv { get => _fv; set { _fv=value; OnPropertyChanged(); SortProducts(0); } } // Value of First Slider.
	public int Sv { get => _sv; set { _sv=value; OnPropertyChanged(); SortProducts(0); } } // Value of Second Slider.
	public string SearchText { get => _searchText; set { _searchText=value; OnPropertyChanged(); SortProducts(0); } } // Value of Search Text Box.

	public AppDbContext Db { get => App.Container!.GetInstance<AppDbContext>(); } // Main DB Context of App.

	public HomePageViewModel()
	{
		AddCommand = new RelayCommand<object>(AddCommandExecute);
		ShowCommand = new RelayCommand<object>(ShowCommandExecute);
		Adm = new RelayCommand<object>(AdmExec);

		BackCommand = new RelayCommand<object>(BackCommandExecute);
		CartPageCommand = new RelayCommand<object>(CartPageCommandExecute);
		ProfilePageCommand = new RelayCommand<object>(ProfilePageCommandExecute);

		//u1 = new User()
		//{
		//	Address="Address",
		//	Cart = new() { CartItems=new List<CartItem>() },
		//	Email = "Email",
		//	FirstName = "FirstName",
		//	LastName = "LastName",
		//	Orders = new List<Order>(),
		//	PasswordHash = "PasswordHash",
		//	PhoneNumber = "PhoneNumber",
		//	Role = false,
		//	Username = "Username"
		//};


		Fv = Min;
		Sv = Max;

		Db.ProductImages.Load();
		Db.Products.Load();
		Db.Users.Load();

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

			if (Ps is null)
				Ps = Db.Products.ToList();

			if (index == 0)
				Ps = Db.Products.OrderBy(p => p.Name).ToList();
			else if (index == 1)
				Ps = Db.Products.OrderBy(p => p.Price).ToList();
			else if (index == 2)
				Ps = Db.Products.OrderByDescending(p => p.Name).ToList();
			else if (index == 3)
				Ps = Db.Products.OrderByDescending(p => p.Price).ToList();

			//else if (index == 4) // price sorting
			//	Ps = Db.Products.Where(product => product.Price >= Fv && product.Price <= Sv).ToList();
			//else if (index == 5) // product searching
			//	Ps = Db.Products.Where(product => product.Name.StartsWith(_searchText)).ToList();

			Ps = Ps.Where(product => product.Price >= Fv && product.Price <= Sv).ToList();

			if (_searchText is not null && _searchText != "Search...")
				Ps = Ps.Where(product => product.Name.Contains(_searchText)).ToList();
			//Ps = Ps.Where(product => product.Name.StartsWith(_searchText)).ToList();

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

		if (t.Count < 1)
			return 100;
		else
			return ((int)t.Max(p => p.Price))+1;

	}

	#region Cart Page Command

	public ICommand CartPageCommand { get; set; }
	public void CartPageCommandExecute(object? obj)
	{
		try
		{
			var cart = App.Container!.GetInstance<CartPageView>();

			var vm = App.Container.GetInstance<CartPageViewModel>();
			vm.FirstLoad();

			cart.DataContext = vm;

			App.Container
			.GetInstance<MainWindowView>()
			.MainContentFrame
			.Navigate(cart);
		}
		catch
		{
			MessageBox.Show("Error in Cart Page Command", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

	#region Profile Page Command

	public ICommand ProfilePageCommand { get; set; }
	public void ProfilePageCommandExecute(object? obj)
	{
		try
		{
			App.Container!
			.GetInstance<MainWindowView>()
			.MainContentFrame
			.Navigate(App.Container.GetInstance<ProfilePageView>());
		}
		catch
		{
			MessageBox.Show("Error in Profile Page Command", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

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
