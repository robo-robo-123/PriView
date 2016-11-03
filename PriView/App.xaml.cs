using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PriView;
using Windows.UI.Core;

namespace PriView
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

      // 中断されるときのイベント
      Application.Current.Suspending += Application_Suspending;

      // 中断から再開されたときのイベント
      Application.Current.Resuming += Application_Resuming;


      // ウィンドウの表示／非表示が切り替わったときのイベント
      //Window.Current.VisibilityChanged += Window_VisibilityChanged;

    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="e">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
      Frame rootFrame = Window.Current.Content as Frame;

      // Do not repeat app initialization when the Window already has content - rather just ensure that the window is active
      if (rootFrame == null)
      {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        rootFrame.NavigationFailed += OnNavigationFailed;

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          //TODO: Load state from previously suspended application
        }

        if (!e.PrelaunchActivated)
        {
          // TODO: This is not a prelaunch activation. Perform operations which
          // assume that the user explicitly launched the app such as updating
          // the online presence of the user on a social network, updating a
          // what's new feed, etc.
        }

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
      }

      this.UpdateBackButtonState();
      rootFrame.Navigated += (_, __) => this.UpdateBackButtonState();
      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (rootFrame.CanGoBack)
        {
          rootFrame.GoBack();
          args.Handled = true;
        }
      };

      if (rootFrame.Content == null)
      {
        Model.GeneralModel.Instance.LoadCount();
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation parameter
        rootFrame.Navigate(typeof(MSPassPage), e.Arguments);
      }
      // Ensure the current window is active
      Window.Current.Activate();
    }

    /// <summary>
    /// Invoked when Navigation to a certain page fails
    /// </summary>
    /// <param name="sender">The Frame which failed navigation</param>
    /// <param name="e">Details about the navigation failure</param>
    void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

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
      //TODO: Save application state and stop any background activity
      Model.GeneralModel.Instance.SaveCount();

      deferral.Complete();
        }

    protected override void OnActivated(IActivatedEventArgs e)
    {
      Frame rootFrame = Window.Current.Content as Frame;
      rootFrame.Navigate(typeof(MSPassPage));

    }

    // 中断されるとき
    private void Application_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
    {

    }

    // 中断から再開されたとき
    private void Application_Resuming(object sender, object e)
    {
      Frame rootFrame = Window.Current.Content as Frame;
      rootFrame.Navigate(typeof(MSPassPage));
    }

    /*
    private void Window_VisibilityChanged(object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
    {
      if (!e.Visible)
      {
        // 非表示にされるとき
      }
      else
      {
        // 表示されるとき
        Frame rootFrame = Window.Current.Content as Frame;
        rootFrame.Navigate(typeof(MSPassPage));
      }
    }
    */

    private void UpdateBackButtonState()
    {
      var rootFrame = (Frame)Window.Current.Content;
      SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack ?
          AppViewBackButtonVisibility.Visible :
          AppViewBackButtonVisibility.Collapsed;

    }


  }
}
