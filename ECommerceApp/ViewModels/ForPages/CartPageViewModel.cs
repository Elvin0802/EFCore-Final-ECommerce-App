using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;

namespace ECommerceApp.ViewModels.ForPages;

public class CartPageViewModel : BaseViewModel
{
	public Cart? Cart { get; set; } = null;

	public CartPageViewModel()
	{
		BackCommand = new RelayCommand<object>(BackCommandExecute);
	}

	public void Load()
	{
		try
		{
			Cart = App.Container!.GetInstance<ProfilePageViewModel>().User.Cart;

			Cart.CartItems = Cart.CartItems == null ? new List<CartItem>() : Cart.CartItems;

			App.Container!.GetInstance<AppDbContext>().SaveChanges();

			App.Container!.GetInstance<CartPageView>().CartView.ItemsSource = Cart.CartItems == null ? new List<CartItem>() : Cart.CartItems;

			App.Container!.GetInstance<CartPageView>().CartView.Items.Refresh();
		}
		catch
		{
			Cart = null;
		}
	}

}
