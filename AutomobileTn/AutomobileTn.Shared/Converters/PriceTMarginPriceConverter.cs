using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace AutomobileTn.Converters
{
	public class PriceTMarginPriceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var price = (double) value;

			var marginPercentage = (int) parameter;

			var marginPrice = price * marginPercentage / 100;

			return price + marginPrice;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
