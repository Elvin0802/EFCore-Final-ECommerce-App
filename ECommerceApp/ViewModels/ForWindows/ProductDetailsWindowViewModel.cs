using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForWindows;

//public class ProductDetailsWindowViewModel : BaseViewModel
//{
//	private Product _product;

//	public Product Product { get => _product; set { _product=value; OnPropertyChanged(); } }

//    public ProductDetailsWindowViewModel()
//    {

//    }


//}

public class ProductDetailsWindowViewModel : BaseViewModel
{
	private Product? _product;
	private int _currentImageIndex;

	public Product? Product
	{
		get => _product;
		set
		{
			_product = value;
			OnPropertyChanged(nameof(Product));
			OnPropertyChanged(nameof(CurrentImageUrl));
		}
	}

	public string CurrentImageUrl
	{
		get => Product?.ProductImages?.ElementAtOrDefault(_currentImageIndex)?.ImageUrl;
	}

	public ICommand NextImageCommand { get; }
	public ICommand PreviousImageCommand { get; }

	public ProductDetailsWindowViewModel()
	{
		_currentImageIndex = 0;

		NextImageCommand = new RelayCommand<object>(NextImage, CanExecuteImageChange);
		PreviousImageCommand = new RelayCommand<object>(PreviousImage, CanExecuteImageChange);
	}

	private void NextImage(object? obj)
	{
		if (_currentImageIndex < Product.ProductImages.Count - 1)
		{
			_currentImageIndex++;
			OnPropertyChanged(nameof(CurrentImageUrl));
		}
	}

	private void PreviousImage(object? obj)
	{
		if (_currentImageIndex > 0)
		{
			_currentImageIndex--;
			OnPropertyChanged(nameof(CurrentImageUrl));
		}
	}

	private bool CanExecuteImageChange(object? obj)
	{
		return Product?.ProductImages?.Count > 1;
	}
}
