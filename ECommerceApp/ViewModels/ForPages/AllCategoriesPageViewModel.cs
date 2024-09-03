using ECommerceApp.Commands;
using ECommerceApp.Models.EFCore;
using ECommerceApp.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace ECommerceApp.ViewModels.ForPages;

public class AllCategoriesPageViewModel : BaseViewModel
{
	public AllCategoriesPageViewModel()
	{
		AddNewCategorieCommand = new RelayCommand<object>(AddNewCategorieCommandExecute);
		RemoveCategorieCommand = new RelayCommand<object>(RemoveCategorieCommandExecute);

		RefreshListView();

	}

	public ICommand AddNewCategorieCommand { get; set; }
	public ICommand RemoveCategorieCommand { get; set; }

	public AppDbContext DB { get => App.Container!.GetInstance<AppDbContext>(); }
	public List<Category> AllCategories { get => App.Container!.GetInstance<AppDbContext>().Categories.ToList(); }

	public void AddNewCategorieCommandExecute(object? obj)
	{
		var NewName = App.Container!.GetInstance<AllCategoriesPageView>().CategorieNameTB.Text;

		if (NewName == string.Empty || NewName == "Enter the new categorie name.")
		{
			MessageBox.Show("Category name cannot be empty.", "Categorie", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		if (DB.Categories.Any(c => c.Name == NewName))
		{
			MessageBox.Show("This category already exists.", "Categorie", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		DB.Categories.Add(new Category(NewName));
		DB.SaveChanges();
		RefreshListView();
	}

	public void RemoveCategorieCommandExecute(object? obj)
	{
		var selectedCategory = obj as Category;

		if (selectedCategory is null)
			return;

		DB.Categories.Remove(selectedCategory);
		DB.SaveChanges();
		RefreshListView();
	}

	public void RefreshListView()
	{

		App.Container!.GetInstance<AllCategoriesPageView>().CategoriesView.ItemsSource = AllCategories;
		App.Container!.GetInstance<AllCategoriesPageView>().CategoriesView.Items.Refresh();

	}

}
