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
			Description="Description Adding",
			Name="Name Adding",
			Price=129,
			ProductImages=new List<ProductImage>(),
			ProductReviews = new List<ProductReview>(),
			StockQuantity=100
		};

		TakeProductImageCommand = new RelayCommand<object>(TakeProductImageExecute);
		Com = new RelayCommand<object>(ComExecute);





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

	#region Complete

	public ICommand Com { get; set; }
	private void ComExecute(object? obj)
	{
		try
		{
			App.Container!.GetInstance<AppDbContext>().Products.Add(Product);
			Product = new();

			App.Container!.GetInstance<AppDbContext>().SaveChanges();

			MessageBox.Show(
			App.Container!.GetInstance<AppDbContext>().Products.Count().ToString());

			BackCommandExecute(obj);

		}
		catch { MessageBox.Show("Error in Complete Command."); }
	}

	#endregion

}
