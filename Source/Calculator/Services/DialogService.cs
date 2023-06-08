using Calculator.ViewModels.Services;
using Microsoft.UI.Xaml.Controls;

namespace Calculator.Services
{
    public class DialogService : IDialogService
    {
        public static DialogService Instance { get; } = new();

        private DialogService()
        {
        }

        public async Task ShowDialogAsync(string title, string message)
        {
            ContentDialog contentDialog = new ContentDialog {
                Title = title,
                Content = message,
            };

            await contentDialog.ShowAsync();
        }
    }
}