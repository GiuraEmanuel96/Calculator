namespace Calculator.ViewModels.Services
{
    public interface IDialogService
    {
        Task ShowDialogAsync(string title, string message);
    }
}