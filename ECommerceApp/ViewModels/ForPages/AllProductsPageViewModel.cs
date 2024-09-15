using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AllProductsPageViewModel : BaseViewModel
{
	public AllProductsPageViewModel()
	{
		AddNewProductCommand = new RelayCommand<object>(AddNewProductCommandExecute);
		BackCommand = new RelayCommand<object>(BackCommandExecute);

		Ps = App.Container.GetInstance<AppDbContext>().Products.ToList();
	}

	private List<Product> ps;
	public List<Product> Ps { get => ps; set { ps=value; OnPropertyChanged(); } }

	public ICommand AddNewProductCommand { get; set; }

	public void AddNewProductCommandExecute(object? obj)
	{

		var p = App.Container!.GetInstance<AddProductPageView>();

		var vm = App.Container.GetInstance<AddProductPageViewModel>();
		vm.RefreshPage();

		p.DataContext = vm;

		App.Container!
				.GetInstance<MainWindowView>()
				.MainContentFrame
				.Navigate(p);
	}

}
