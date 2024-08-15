using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;

namespace ECommerceApp.ViewModels.ForPages;

public class CartPageViewModel : BaseViewModel
{
	public Cart? Cart { get; set; } = null;

	public CartPageViewModel()
	{
		Cart = App.Container!.GetInstance<HomePageViewModel>().u1.Cart as Cart;

		App.Container!.GetInstance<CartPageView>().CartView.ItemsSource = Cart.CartItems;

		App.Container!.GetInstance<CartPageView>().CartView.Items.Refresh();
	}

}
