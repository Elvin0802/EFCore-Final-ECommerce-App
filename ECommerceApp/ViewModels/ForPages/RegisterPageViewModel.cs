using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Services.Additional;
using ECommerceApp.Views.Pages;
using ECommerceApp.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class RegisterPageViewModel : BaseViewModel
{
	public RegisterPageViewModel()
	{
		BackCommand = new RelayCommand<object>(BackCommandExecute);
		RegisterCommand = new RelayCommand<object>(RegisterCommandExecute);
		VerifyEmailCommand = new RelayCommand<object>(VerifyEmailCommandExecute);


		User = new();
		User.Role=false;
		User.Cart = new() { CartItems = new List<CartItem>() };
		User.Orders = new List<Order>();
	}

	private User _user;
	public User User
	{
		get => _user;
		set { _user = value; OnPropertyChanged(); }
	}
	private string _sendedAuthCode;
	private string _authCode;
	public string AuthCode
	{
		get => _authCode;
		set { _authCode = value; OnPropertyChanged(); }
	}

	public ICommand RegisterCommand { get; set; }

	public void RegisterCommandExecute(object? obj)
	{
		if (AuthCode != _sendedAuthCode)
		{
			MessageBox.Show("Please, enter correct auth code.", "Register Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		if (User is null)
		{
			MessageBox.Show("Please, enter data of new user.", "Register Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}
		User.PasswordHash = App.Container.GetInstance<RegisterPageView>().PasswordB.Password;

		var db = App.Container!.GetInstance<AppDbContext>();

		db.Users.Load();
		db.Carts.Load();
		db.CartItems.Load();

		db.Users.Add(User);

		db.SaveChanges();

		User = new();

		User.Role=false;
		User.Cart = new() { CartItems = new List<CartItem>() };
		User.Orders = new List<Order>();

		App.Container!.GetInstance<MainWindowView>().MainContentFrame.Navigate(App.Container.GetInstance<LoginPageView>());
	}
	public ICommand VerifyEmailCommand { get; set; }

	private void VerifyEmailCommandExecute(object? obj)
	{
		try
		{
			_sendedAuthCode = Random.Shared.Next(1001, 9998).ToString();

			EmailService.SendNotificationToEmail($"Ecommerce App Email Auth.", $"Your auth code : {_sendedAuthCode}", User!.Email!);

			MessageBox.Show("Auth code sended to your email.");
		}
		catch { MessageBox.Show("Error in Code Sender"); }
	}




}
