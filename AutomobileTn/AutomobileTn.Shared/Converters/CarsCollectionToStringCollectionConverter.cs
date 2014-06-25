using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Data;
using AutomobileTn.Models;

namespace AutomobileTn.Converters
{
    public class CarsCollectionToStringCollectionConverter : IValueConverter
    {
	    public object Convert(object value,
		    Type targetType,
		    object parameter,
		    string language)
	    {
		    var carsCollection = value as List<Car>;

		    if (carsCollection == null) return null;

			var result = carsCollection.Select(car => car.Model).ToList();

		    return result;
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
