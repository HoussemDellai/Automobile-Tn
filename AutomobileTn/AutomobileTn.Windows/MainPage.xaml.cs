using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AutomobileTn
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

			NavigationCacheMode = NavigationCacheMode.Required;

		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			// TODO: Prepare page for display here.

			// TODO: If your application contains multiple pages, ensure that you are
			// handling the hardware Back button by registering for the
			// Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
			// If you are using the NavigationHelper provided by some templates,
			// this event is handled for you.

			// If there is a parameter then continue the navigation to the appropriate page.
			if (!string.IsNullOrWhiteSpace(e.Parameter.ToString()))
			{
				var launchParam = e.Parameter.ToString();
				//var message = new MessageDialog(launchParam);
				//message.ShowAsync();
			}

			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
		}

		private async void SearchAppBarButton_Click(object sender, RoutedEventArgs e)
		{
			SearchSection.Visibility = Visibility.Visible;
			await Task.Delay(200);
			MainHub.ScrollToSection(SearchSection);

		}
	}
}
