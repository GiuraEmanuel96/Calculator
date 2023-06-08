// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Calculator.Pages;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Calculator;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    ///
    private Frame _rootFrame;

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        window = new Window();
        window.Activate();

        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        appWindow.Closing += OnAppClosing;

        if (window.Content is Frame frame)
        {
            _rootFrame = frame;
        }
        else
        {
            // Handle the case where window.Content is not a Frame.
        }

        if (_rootFrame == null)
        {
            // Create a Frame to act as the navigation context and navigate to the first page
            _rootFrame = new Frame();
            window.Content = _rootFrame;
        }

        if (_rootFrame.Content == null)
            _rootFrame.Navigate(typeof(MainPage), args.Arguments);
    }

    private async void OnAppClosing(AppWindow sender, AppWindowClosingEventArgs args)
    {
        args.Cancel = true;

        // do your async save work here
        if (_rootFrame.Content is ISaveStatePage saveStatePage)
            await saveStatePage.ViewModel.SaveAsync();

        window.Close();
    }

    private Window window;
}