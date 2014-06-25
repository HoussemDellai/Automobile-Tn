using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace AutomobileTn.Converters
{
    public class IntToPriceConverter : IValueConverter
    {
	    public object Convert(object value,
		    Type targetType,
		    object parameter,
		    string language)
	    {
		    if (!(value is int)) return value;

		    var number = (int) value;

		    return number/1000 + " k DT";
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
