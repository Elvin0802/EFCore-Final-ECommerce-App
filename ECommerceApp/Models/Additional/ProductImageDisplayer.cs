using ECommerceApp.Models.EFCore;
using ECommerceApp.Services.Additional;

namespace ECommerceApp.Models.Additional;

public class ProductImageDisplayer : NotifyService
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

	public ProductImageDisplayer()
	{
		ResetIndex();
	}

	public void NextImage(object? obj)
	{
		if (_currentImageIndex < Product.ProductImages.Count - 1)
		{
			_currentImageIndex++;
			OnPropertyChanged(nameof(CurrentImageUrl));
		}
	}

	public void PreviousImage(object? obj)
	{
		if (_currentImageIndex > 0)
		{
			_currentImageIndex--;
			OnPropertyChanged(nameof(CurrentImageUrl));
		}
	}

	public bool CanExecuteImageChange(object? obj)
	{
		return Product?.ProductImages?.Count > 1;
	}

	public void ResetIndex() => _currentImageIndex=0;

}
