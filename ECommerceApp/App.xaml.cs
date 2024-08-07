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

		RegisterOfViews();
		RegisterOfViewModels();

		var Window = Container!.GetInstance<MainWindowView>();
		Window.DataContext = Container.GetInstance<MainWindowViewModel>();

		var Page = Container!.GetInstance<LoginPageView>();
		Page.DataContext = Container.GetInstance<LoginPageViewModel>();

		Window.MainContentFrame.Navigate(Page);

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
	public void RegisterOfViews()
	{
			/**** Windows ****/

		Container?.RegisterSingleton<MainWindowView>();


			/**** Pages  ****/

		Container?.RegisterSingleton<LoginPageView>();
		Container?.RegisterSingleton<RegisterPageView>();


	}
	public void RegisterOfViewModels()
	{
			/**** Windows ****/

		Container?.RegisterSingleton<MainWindowViewModel>();


			/**** Pages  ****/

		Container?.RegisterSingleton<LoginPageViewModel>();
		Container?.RegisterSingleton<RegisterPageViewModel>();


	}

}
