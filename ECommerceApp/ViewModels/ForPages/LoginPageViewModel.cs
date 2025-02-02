﻿using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class LoginPageViewModel : BaseViewModel
{
	public LoginPageViewModel()
	{
		LoginCommand = new RelayCommand<object>(LoginCommandExecute);
		RegisterCommand = new RelayCommand<object>(RegisterCommandExecute);



	}

	public ICommand LoginCommand { get; set; }
	public ICommand RegisterCommand { get; set; }

	public void LoginCommandExecute(object? obj)
	{
		var db = App.Container!.GetInstance<AppDbContext>();

		db.CartItems.Load();
		db.Carts.Load();
		db.Users.Load();

		var e = App.Container.GetInstance<LoginPageView>().EmailText.Text;
		var p = App.Container.GetInstance<LoginPageView>().PasswordText.Text;

		foreach (var item in db.Users.ToList())
		{
			if (item.Email == e && item.PasswordHash == p)
			{
				var page = obj as Page;

				var home = App.Container.GetInstance<HomePageView>();

				var vm = App.Container.GetInstance<HomePageViewModel>();
				vm.u1 = item;

				App.Container.GetInstance<ProfilePageViewModel>().User = item;

				home.DataContext = vm;

				this.StartLoadingAnimation(4, home);

				break;
			}
		}
	}

	public void RegisterCommandExecute(object? obj)
	{
		var page = obj as Page;

		var r = App.Container!.GetInstance<RegisterPageView>();
		r.DataContext = App.Container.GetInstance<RegisterPageViewModel>();

		page?.NavigationService.Navigate(r);
	}



}
