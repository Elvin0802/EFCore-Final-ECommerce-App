using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;

namespace ECommerceApp.ViewModels.ForPages;

public class ProfilePageViewModel : BaseViewModel
{

	public User User { get; set; }

	public ProfilePageViewModel()
	{
		BackCommand = new RelayCommand<object>(BackCommandExecute);
	}



}
