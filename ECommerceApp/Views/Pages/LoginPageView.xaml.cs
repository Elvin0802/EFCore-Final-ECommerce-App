using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ECommerceApp.Views.Pages;

public partial class LoginPageView : Page
{
	public LoginPageView()
	{
		InitializeComponent();
	}
	private void PlaceholderTextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		if (textBox.Text == "Enter your text here...")
		{
			textBox.Text = "";
			textBox.Foreground = Brushes.Black;
		}
	}

	private void PlaceholderTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		if (string.IsNullOrWhiteSpace(textBox.Text))
		{
			textBox.Text = "Enter your text here...";
			textBox.Foreground = Brushes.Gray;
		}
	}

}
