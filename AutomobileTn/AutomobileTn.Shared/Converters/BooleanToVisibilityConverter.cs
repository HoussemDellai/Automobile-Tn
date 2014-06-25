using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AutomobileTn.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter	
    {
	    public object Convert(object value,
		    Type targetType,
		    object parameter,
		    string language)
	    {
		    var isTrue = (bool) value;

		    if (isTrue)
		    {
			    return Visibility.Visible;
		    }

		    return Visibility.Collapsed;
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
