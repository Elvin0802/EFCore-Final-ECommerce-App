using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AdminPageViewModel : BaseViewModel
{

	public ICommand CategoriesCommand { get; set; }
	public ICommand ProductsCommand { get; set; }
	public ICommand HP { get; set; }

	public AdminPageViewModel()
	{
		CategoriesCommand = new RelayCommand<object>(CategoriesCommandExecute);
		ProductsCommand = new RelayCommand<object>(ProductsCommandExecute);
		HP = new RelayCommand<object>(hp);
		LoadProductCount();
	}

	public void CategoriesCommandExecute(object? obj)
	{
		var p = App.Container!.GetInstance<AllCategoriesPageView>();
		p.DataContext = App.Container.GetInstance<AllCategoriesPageViewModel>();

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}
	public void ProductsCommandExecute(object? obj)
	{
		var p = App.Container!.GetInstance<AllProductsPageView>();
		p.DataContext = App.Container.GetInstance<AllProductsPageViewModel>();

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}
	public void hp(object? obj)
	{
		var p = App.Container!.GetInstance<HomePageView>();
		p.DataContext = App.Container.GetInstance<HomePageViewModel>();

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}


	private void LoadProductCount() => App.Container!
										.GetInstance<AdminPageView>()
										.ProductCountText
										.Text = GetTotalProductCountFromDatabase()
												.ToString();

	private int GetTotalProductCountFromDatabase() => App.Container!
														.GetInstance<AppDbContext>()
														.Products
														.Count();






}

