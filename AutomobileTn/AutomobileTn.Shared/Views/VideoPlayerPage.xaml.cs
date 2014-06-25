using Windows.Graphics.Display;
#if WINDOWS_PHONE_APP || WINDOWS_APP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#endif

#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AutomobileTn.Shared.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class VideoPlayerPage
	{
		public VideoPlayerPage()
		{
			InitializeComponent();
		}
#if WINDOWS_PHONE_APP
		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
			//DisplayInformation.GetForCurrentView().OrientationChanged += OnOrientationChanged;

			HardwareButtons.BackPressed += HardwareButtons_BackPressed;


			base.OnNavigatedTo(e);
		}

		void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
		{
			var rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null && rootFrame.CanGoBack)
			{
				rootFrame.GoBack();
				e.Handled = true;
			}
		}
#endif
	}
}
