using System.Windows;
using System.Windows.Data;

namespace ChatApp.Wpf.Converters;

public class UnreadCountToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{
		var unreadCount = (int)value;
		return unreadCount > 0 ? Visibility.Visible : Visibility.Hidden;
	}

	public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
