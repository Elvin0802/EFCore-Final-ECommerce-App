using ECommerceApp.Commands;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AdminPageViewModel : BaseViewModel
{

	public ICommand AC { get; set; }

	public AdminPageViewModel()
	{
		AC = new RelayCommand<object>(aa);
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
}
