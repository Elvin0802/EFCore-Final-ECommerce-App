using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AdminPageViewModel : BaseViewModel
{

	public ICommand AC { get; set; }
	public ICommand HP { get; set; }

	public AdminPageViewModel()
	{
		AC = new RelayCommand<object>(aa);
		HP = new RelayCommand<object>(hp);
		LoadProductCount();
	}

	public void aa(object? obj)
	{
		var p = App.Container!.GetInstance<AddProductPageView>();
		p.DataContext = App.Container.GetInstance<AddProductPageViewModel>();

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

