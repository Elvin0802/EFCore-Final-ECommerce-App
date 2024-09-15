using ECommerceApp.Commands;
using ECommerceApp.Models.Additional;
using ECommerceApp.Models.EFCore;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForWindows;

public class ProductDetailsWindowViewModel : BaseViewModel
{
	private Product? _product;

	public Product? Product
	{
		get => _product;
		set
		{
			_product = value;
			OnPropertyChanged(nameof(Product));
			RefreshPage();
		}
	}

	public ProductImageDisplayer ImageDisplayer { get; set; }

	public ICommand NextImageCommand { get; set; }
	public ICommand PreviousImageCommand { get; set; }

	public ProductDetailsWindowViewModel()
	{
		ImageDisplayer = new ProductImageDisplayer();

		RefreshPage();

		NextImageCommand = new RelayCommand<object>(ImageDisplayer.NextImage, ImageDisplayer.CanExecuteImageChange);
		PreviousImageCommand = new RelayCommand<object>(ImageDisplayer.PreviousImage, ImageDisplayer.CanExecuteImageChange);
	}
	public void RefreshPage()
	{
		ImageDisplayer.ResetIndex();
		ImageDisplayer!.Product = Product;
	}
}
