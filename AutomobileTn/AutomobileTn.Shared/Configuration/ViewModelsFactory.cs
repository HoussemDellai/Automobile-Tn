using AutomobileTn.ViewModels;

namespace AutomobileTn.Configuration
{
    public class ViewModelsFactory
    {
	    private MainViewModel _mainViewModel;

	    public MainViewModel MainViewModel
	    {
		    get
		    {
			    if (_mainViewModel == null)
			    {
				    _mainViewModel = new MainViewModel();
			    }
			    return _mainViewModel;
		    }
	    }

	    public VideoViewModel VideoViewModel { get; set; }
    }
}
