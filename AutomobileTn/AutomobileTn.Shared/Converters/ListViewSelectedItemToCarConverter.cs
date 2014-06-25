using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using AutomobileTn.Models;

namespace AutomobileTn.Converters
{
	class ListViewSelectedItemToCarConverter : IValueConverter
	{
		public object Convert(object value,
			Type targetType,
			object parameter,
			string language)
		{
			var listView = value as ListView;

			if (listView == null)
				return null;

			var selectedCar = listView.SelectedItem as Car;

			return selectedCar;
		}

		public object ConvertBack(object value,
			Type targetType,
			object parameter,
			string language)
		{
			throw new NotImplementedException();
		}
	}
}
