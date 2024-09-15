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
		FirstLoad();
	}

	public void FirstLoad()
	{
		try
		{
			Cart = App.Container!.GetInstance<HomePageViewModel>().u1.Cart as Cart;

			App.Container!.GetInstance<CartPageView>().CartView.ItemsSource = Cart.CartItems;

			App.Container!.GetInstance<CartPageView>().CartView.Items.Refresh();
		}
		catch
		{
			Cart = null;
		}
	}

}
