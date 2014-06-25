using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Data;

namespace AutomobileTn.Converters
{
	public class ListToSubListConverter : IValueConverter
	{
		public object Convert(object value,
			Type targetType,
			object parameter,
			string language)
		{
			var collection = value as IEnumerable<object>;

			if (collection == null)
				return value;

			var numberOfItemsToSkip = int.Parse((string) parameter);

			return collection.Skip(numberOfItemsToSkip).Take(6);
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
