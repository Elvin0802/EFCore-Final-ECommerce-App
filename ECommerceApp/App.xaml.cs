using ECommerceApp.Views.Windows;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Configuration;
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



		_ = new MainWindow().ShowDialog();


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
	//	Container?.RegisterSingleton<MainWindowView>();

			/**** Windows ****/


			/**** Pages  ****/



	}
	public void RegisterOfViewModels()
	{
		//	Container?.RegisterSingleton<MainWindowViewModel>();

			/**** Windows ****/


			/**** Pages  ****/
	}

}
