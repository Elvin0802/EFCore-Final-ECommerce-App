using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using System.Windows.Forms;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AddProductPageViewModel : BaseViewModel
{
	private Product _product;

	public Product Product { get => _product; set { _product = value; OnPropertyChanged(); } }
	public int _i;
	public int ImageCount { get => _i; set { _i =value; OnPropertyChanged(); } }
	//public int ImageCount { get => Product.ProductImages.Count; }

	public AddProductPageViewModel()
	{
		Product = new()
		{
			CategoryId=1,
			Description="Description",
			Name="Name",
			Price=99,
			ProductImages=new List<ProductImage>(),
			ProductReviews = new List<ProductReview>(),
			StockQuantity=100
		};

		TakeProductImageCommand = new RelayCommand<object>(TakeProductImageExecute);






	}

	#region Take Product Image

	public ICommand TakeProductImageCommand { get; set; }
	private void TakeProductImageExecute(object? obj)
	{
		try
		{

			var t = GetImage()!;

			Product!.ProductImages.Add(new() { ImageUrl = t });

			ImageCount = Product.ProductImages.Count;
		}
		catch { MessageBox.Show("Error in Take Product Image Command."); }
	}

	#endregion

}
