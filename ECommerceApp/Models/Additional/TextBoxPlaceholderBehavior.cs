using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ECommerceApp.Models.Additional;

public static class TextBoxPlaceholderBehavior
{
	public static readonly DependencyProperty PlaceholderTextProperty =
		DependencyProperty.RegisterAttached(
			"PlaceholderText",
			typeof(string),
			typeof(TextBoxPlaceholderBehavior),
			new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

	public static string GetPlaceholderText(DependencyObject obj) =>
		(string)obj.GetValue(PlaceholderTextProperty);

	public static void SetPlaceholderText(DependencyObject obj, string value) =>
		obj.SetValue(PlaceholderTextProperty, value);

	private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TextBox textBox)
		{
			textBox.GotFocus -= RemovePlaceholder;
			textBox.LostFocus -= AddPlaceholder;

			if (!string.IsNullOrEmpty(GetPlaceholderText(textBox)))
			{
				textBox.GotFocus += RemovePlaceholder;
				textBox.LostFocus += AddPlaceholder;
				AddPlaceholder(textBox, null); // Initial state
			}
		}
	}

	private static void AddPlaceholder(object sender, RoutedEventArgs e)
	{
		if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
		{
			textBox.Text = GetPlaceholderText(textBox);
			textBox.Foreground = Brushes.Gray;
		}
	}

	private static void RemovePlaceholder(object sender, RoutedEventArgs e)
	{
		if (sender is TextBox textBox && textBox.Text == GetPlaceholderText(textBox))
		{
			textBox.Text = string.Empty;
			textBox.Foreground = Brushes.Black;
		}
	}
}