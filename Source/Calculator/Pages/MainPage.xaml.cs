// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Calculator.Services;
using Calculator.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, ISaveStatePage
    {
        public MainViewModel ViewModel { get; }

        ISaveStateViewModel ISaveStatePage.ViewModel => ViewModel;

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(FileDataManager.Instance, DialogService.Instance);
        }

        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }
}