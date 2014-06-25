﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#endif
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace AutomobileTn
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public sealed partial class App : Application
	{
#if WINDOWS_PHONE_APP
		private TransitionCollection transitions;
#endif

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += this.OnSuspending;
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used when the application is launched to open a specific file, to display
		/// search results, and so forth.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = false;

				// Break whenever there's a Binding error
				DebugSettings.BindingFailed +=
					delegate (object sender,
					BindingFailedEventArgs args) // args.Message
				{

					//Debugger.Break();
				};
			}
#endif

			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();

				// TODO: change this value to a cache size that is appropriate for your application
				rootFrame.CacheSize = 1;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					// TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
			{
#if WINDOWS_PHONE_APP
				// Removes the turnstile navigation for startup.
				if (rootFrame.ContentTransitions != null)
				{
					this.transitions = new TransitionCollection();
					foreach (var c in rootFrame.ContentTransitions)
					{
						this.transitions.Add(c);
					}
				}

				rootFrame.ContentTransitions = null;
				rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

				// When the navigation stack isn't restored navigate to the first page,
				// configuring the new page by passing required information as a navigation
				// parameter
				if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
				{
					throw new Exception("Failed to create initial page");
				}
			}

			// Ensure the current window is active
			Window.Current.Activate();
		}

#if WINDOWS_PHONE_APP
		/// <summary>
		/// Restores the content transitions after the app has launched.
		/// </summary>
		/// <param name="sender">The object where the handler is attached.</param>
		/// <param name="e">Details about the navigation event.</param>
		private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
		{
			var rootFrame = sender as Frame;
			rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
			rootFrame.Navigated -= this.RootFrame_FirstNavigated;
		}
#endif

#if WINDOWS_APP

		private const double SettingsWidth = 370;
		Popup _settingsPopup;

		protected override void OnWindowCreated(WindowCreatedEventArgs args)
		{
			SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
		}

		void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			UICommandInvokedHandler handler = OnSettingsCommand;

			//var appSettingCommand = new SettingsCommand("AS", "Options", handler);
			var privacySettingCommand = new SettingsCommand("PS", "Privacy settings", handler);

			args.Request.ApplicationCommands.Add(privacySettingCommand);
		}

		private void OnSettingsCommand(IUICommand command)
		{
			Rect windowBounds = Window.Current.Bounds;
			_settingsPopup = new Popup();
			_settingsPopup.Closed += SettingsPopup_Closed;
			Window.Current.Activated += Current_Activated;
			_settingsPopup.IsLightDismissEnabled = true;
			SettingsFlyout settingPage = null;

			switch (command.Id.ToString())
			{

				case "PS":
					Launcher.LaunchUriAsync(new Uri("http://houssem.azurewebsites.net/automobiletn.html"));
					break;
					//case "AS":
					//	settingPage = new AppSettings();
					//	break;
			}
			_settingsPopup.Width = SettingsWidth;
			_settingsPopup.Height = windowBounds.Height;

			// Add the proper animation for the panel.
			_settingsPopup.ChildTransitions = new TransitionCollection
				{
					new PaneThemeTransition
						{
							Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right)
									   ? EdgeTransitionLocation.Right
									   : EdgeTransitionLocation.Left
						}
				};
			if (settingPage != null)
			{
				settingPage.Width = SettingsWidth;
				settingPage.Height = windowBounds.Height;
			}

			// Place the SettingsFlyout inside our Popup window.
			_settingsPopup.Child = settingPage;

			// Let's define the location of our Popup.
			_settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - SettingsWidth) : 0);
			_settingsPopup.SetValue(Canvas.TopProperty, 0);
			_settingsPopup.IsOpen = true;
		}

		void Current_Activated(object sender, WindowActivatedEventArgs e)
		{
			if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
			{
				_settingsPopup.IsOpen = false;
			}
		}

		void SettingsPopup_Closed(object sender, object e)
		{
			Window.Current.Activated -= Current_Activated;
		}

#endif

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();

			// TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}