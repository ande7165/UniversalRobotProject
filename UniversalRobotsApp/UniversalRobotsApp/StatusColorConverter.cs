using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URModels;

namespace UniversalRobotsApp
{
	public class StatusColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((Status)value)
			{
				case Status.OK:
					return new SolidColorBrush(new Color(0, 255, 0));
				case Status.Warning:
					return new SolidColorBrush(new Color(255, 255, 0));
				case Status.Error:
					return new SolidColorBrush(new Color(255, 0, 0));
				default:
					return new SolidColorBrush(new Color(0, 255, 0));
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
