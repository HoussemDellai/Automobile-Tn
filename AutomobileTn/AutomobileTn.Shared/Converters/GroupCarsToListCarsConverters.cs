using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using AutomobileTn.Models;

namespace AutomobileTn.Converters
{
    public class GroupCarsToListCarsConverters : IValueConverter
    {
	    public object Convert(object value,
		    Type targetType,
		    object parameter,
		    string language)
	    {
		    var groupCars = value as GroupCars;

		    if (groupCars == null) return value;

		    return groupCars.CarsCollection;
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
