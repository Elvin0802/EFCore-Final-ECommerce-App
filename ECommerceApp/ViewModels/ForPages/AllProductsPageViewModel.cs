using ECommerceApp.Commands;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AllProductsPageViewModel : BaseViewModel
{
	public AllProductsPageViewModel()
	{
		AddNewProductCommand = new RelayCommand<object>(AddNewProductCommandExecute);

	}


	public ICommand AddNewProductCommand { get; set; }

	public void AddNewProductCommandExecute(object? obj)
	{

		var p = App.Container!.GetInstance<AddProductPageView>();
		p.DataContext = App.Container.GetInstance<AddProductPageViewModel>();

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}

}
