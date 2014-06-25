using System;
using System.Collections.Generic;
using System.Text;
using AutomobileTn.Models;

namespace AutomobileTn.ViewModels
{
    public class VideoViewModel : BaseViewModel
	{
	    private Video _selectedVideo;

	    public Video SelectedVideo
	    {
		    get { return _selectedVideo; }
		    set
		    {
			    _selectedVideo = value;
			    OnPropertyChanged();
		    }
	    }

	    public VideoViewModel()
	    {
		    
	    }
    }
}
