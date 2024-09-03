using ECommerceApp.Models.EFCore;
using ECommerceApp.ViewModels.ForPages;
using ECommerceApp.ViewModels.ForWindows;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.IO;
using System.Windows;

namespace ECommerceApp;

public partial class App : Application
{
	public static IConfigurationRoot? Configuration { get; set; }
	public static Container? Container { get; set; }


	// Start Method of Application.
	private void OpenMarket(object sender, StartupEventArgs e)
	{
		SetConfiguration();

		RegisterOfModels();
		RegisterOfViews();
		RegisterOfViewModels();

		var Window = Container!.GetInstance<MainWindowView>();
		var MainPage = Container!.GetInstance<HomePageView>();
		var DashboardPage = Container!.GetInstance<DashboardPageView>();

		Window.DataContext = Container.GetInstance<MainWindowViewModel>();
		MainPage.DataContext = Container.GetInstance<HomePageViewModel>();
		DashboardPage.DataContext = Container.GetInstance<DashboardPageViewModel>();

		Window.MainContentFrame.Navigate(MainPage);
		Window.PageNavigationFrame.Navigate(DashboardPage);

		Window.ShowDialog();


		//connStr = App.Configuration!.GetConnectionString("DefaultConnection");
	}

	public void SetConfiguration()
	{
		// Create a new object of SimpleInjector.Container for using Singleton Pattern in App.
		Container = new();

		// Create a new object of MS.Extensions.Configuration for security of special app datas.
		Configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("Resources/ConfigFiles/appsettings.json")
					.Build();
	}
	public void RegisterOfModels()
	{
		Container?.RegisterSingleton<AppDbContext>();
	}
	public void RegisterOfViews()
	{
		/**** Windows ****/

		Container?.RegisterSingleton<MainWindowView>();
		Container?.RegisterSingleton<ProductDetailsWindowView>();


		/**** Pages  ****/

		Container?.RegisterSingleton<LoginPageView>();
		Container?.RegisterSingleton<RegisterPageView>();
		Container?.RegisterSingleton<HomePageView>();
		Container?.RegisterSingleton<DashboardPageView>();
		Container?.RegisterSingleton<CartPageView>();
		Container?.RegisterSingleton<ProfilePageView>();
		Container?.RegisterSingleton<AdminPageView>();
		Container?.RegisterSingleton<AllProductsPageView>();
		Container?.RegisterSingleton<AddProductPageView>();
		Container?.RegisterSingleton<AllCategoriesPageView>();


	}
	public void RegisterOfViewModels()
	{
		/**** Windows ****/

		Container?.RegisterSingleton<MainWindowViewModel>();
		Container?.RegisterSingleton<ProductDetailsWindowViewModel>();


		/**** Pages  ****/

		Container?.RegisterSingleton<LoginPageViewModel>();
		Container?.RegisterSingleton<RegisterPageViewModel>();
		Container?.RegisterSingleton<HomePageViewModel>();
		Container?.RegisterSingleton<DashboardPageViewModel>();
		Container?.RegisterSingleton<CartPageViewModel>();
		Container?.RegisterSingleton<ProfilePageViewModel>();
		Container?.RegisterSingleton<AdminPageViewModel>();
		Container?.RegisterSingleton<AllProductsPageViewModel>();
		Container?.RegisterSingleton<AddProductPageViewModel>();
		Container?.RegisterSingleton<AllCategoriesPageViewModel>();


	}

}
