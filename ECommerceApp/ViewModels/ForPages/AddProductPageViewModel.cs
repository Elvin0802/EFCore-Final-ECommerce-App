using ECommerceApp.Commands;
using ECommerceApp.Models.Additional;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using System.Windows.Forms;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AddProductPageViewModel : BaseViewModel
{
	private Product _product;

	public Product Product { get => _product; set { _product = value; OnPropertyChanged(); } }
	public int _i;
	public int ImageCount { get => _i; set { _i =value; OnPropertyChanged(); } }

	public AddProductPageViewModel()
	{
		ImageDisplayer = new ProductImageDisplayer();

		NextImageCommand = new RelayCommand<object>(ImageDisplayer.NextImage, ImageDisplayer.CanExecuteImageChange);
		PreviousImageCommand = new RelayCommand<object>(ImageDisplayer.PreviousImage, ImageDisplayer.CanExecuteImageChange);

		TakeProductImageCommand = new RelayCommand<object>(TakeProductImageExecute);
		CompleteCommand = new RelayCommand<object>(CompleteCommandExecute);

		RefreshPage();
	}

	public ProductImageDisplayer ImageDisplayer { get; set; }

	public ICommand NextImageCommand { get; set; }
	public ICommand PreviousImageCommand { get; set; }

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

	public ICommand CompleteCommand { get; set; }
	private void CompleteCommandExecute(object? obj)
	{
		try
		{
			var db = App.Container!.GetInstance<AppDbContext>();

			var c = App.Container!.GetInstance<AddProductPageView>().CategoriesCB.SelectedItem as Category;

			Product.Category = c!;
			Product.CategoryId = c!.CategoryId;

			db.Products.Add(Product);

			db.SaveChanges();

			MessageBox.Show(db.Products.Count().ToString());

			Thread.Sleep(2000);

			RefreshPage();

			BackCommandExecute(obj);

		}
		catch { MessageBox.Show("Error in Complete Command."); }
	}

	public void RefreshPage()
	{
		Product = new()
		{
			Description="",
			Name="",
			ProductImages = new List<ProductImage>(),
			Price=1,
			StockQuantity=1,
		};

		App.Container!.GetInstance<AddProductPageView>().CategoriesCB.ItemsSource =
			App.Container.GetInstance<AppDbContext>().Categories.ToList();

		ImageDisplayer.ResetIndex();
		ImageDisplayer!.Product = Product;
	}

	#endregion

}
