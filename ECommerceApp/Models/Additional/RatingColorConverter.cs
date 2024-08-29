using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ECommerceApp.Views.Pages;
public class RatingColorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is double rating)
		{
			if (rating == 1)
				return new SolidColorBrush(Colors.Red);
			if (rating == 5)
				return new SolidColorBrush(Colors.Green);
		}
		return new SolidColorBrush(Colors.Gray); // Default color
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
