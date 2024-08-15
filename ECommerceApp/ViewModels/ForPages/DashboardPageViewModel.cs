using ECommerceApp.Commands;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class DashboardPageViewModel : BaseViewModel
{
	public DashboardPageViewModel()
	{
		ChangeThemeCommand = new RelayCommand<object>(ChangeThemeColor);
		BackCommand = new RelayCommand<object>(BackCommandExecute);
		HomePageCommand = new RelayCommand<object>(HomePageCommandExecute);
		CartPageCommand = new RelayCommand<object>(CartPageCommandExecute);
		ProfilePageCommand = new RelayCommand<object>(ProfilePageCommandExecute);
	}


	#region Home Page Command

	public ICommand HomePageCommand { get; set; }
	public void HomePageCommandExecute(object? obj)
	{
		try
		{
			var page = App.Container.GetInstance<HomePageView>();
			page.DataContext = App.Container!.GetInstance<HomePageViewModel>();



			App.Container!
			.GetInstance<MainWindowView>()
			.MainContentFrame
			.Navigate(page);



		}
		catch
		{
			MessageBox.Show("Error in Home Page Command", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

	#region Cart Page Command

	public ICommand CartPageCommand { get; set; }
	public void CartPageCommandExecute(object? obj)
	{
		try
		{
			App.Container!
			.GetInstance<CartPageView>().DataContext = App.Container
														.GetInstance<CartPageViewModel>();

			App.Container
			.GetInstance<MainWindowView>()
			.MainContentFrame
			.Navigate(App.Container.GetInstance<CartPageView>());
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
}
