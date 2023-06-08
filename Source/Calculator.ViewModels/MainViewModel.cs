using Calculator.Models;
using Calculator.ViewModels.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Techsola;

namespace Calculator.ViewModels
{
    public class MainViewModel : ObservableObject, ISaveStateViewModel
    {
        #region Private fields
        private readonly IDataManager _dataManager;
        private readonly IDialogService _dialogService;
        private int? _firstOperand = 0;
        private int? _secondOperand = 0;
        private Operation _operation;
        private bool _isLoaded;
        #endregion

        public MainViewModel(IDataManager dataManager, IDialogService dialogService)
        {
            _dataManager = dataManager;
            _dialogService = dialogService;
        }

        #region Properties

        public string? ErrorMessage => (_firstOperand == null || _secondOperand == null) ? "Neither operand can be empty." : null;

        public int? FirstOperand
        {
            get => _firstOperand;
            set => SetProperty(ref _firstOperand, value);
        }

        public bool HasErrorMessage => ErrorMessage != null;

        public bool IsLoaded
        {
            get => _isLoaded;
            private set => SetProperty(ref _isLoaded, value);
        }

        public IReadOnlyList<Operation> Operations => new List<Operation> { Operation.Add, Operation.Subtract, Operation.Multiply, Operation.Divide };

        public Operation Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        public int? Result => (_firstOperand.HasValue && _secondOperand.HasValue)
                    ? _operation switch {
                        Operation.Add => _firstOperand.Value + _secondOperand.Value,
                        Operation.Subtract => _firstOperand.Value - _secondOperand.Value,
                        Operation.Multiply => _firstOperand.Value * _secondOperand.Value,
                        Operation.Divide => _secondOperand.Value != 0 ? _firstOperand.Value / _secondOperand.Value : null,
                        _ => throw new NotSupportedException("Invalid operation"),
                    }
                    : null;

        public int? SecondOperand
        {
            get => _secondOperand;
            set => SetProperty(ref _secondOperand, value);
        }

        #endregion

        #region Public methods

        public void Calculate()
        {
            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }

        public void Load()
        {
            AmbientTasks.Add(LoadAsync());

            async Task LoadAsync()
            {
                Calculation data = null;

                try
                {
                    data = await _dataManager.Load();
                }
                catch (IOException ex)
                {
                    await _dialogService.ShowDialogAsync("Error", $"I/O error loading state: {ex.Message}");
                }
                catch (InvalidDataException ex)
                {
                    await _dialogService.ShowDialogAsync($"Error loading saved state: {ex.Message}", "\n\nCorrupt saved state will be deleted.");

                    try
                    {
                       await _dataManager.DeleteAsync();
                    }
                    catch (IOException ioEx)
                    {
                        await _dialogService.ShowDialogAsync("Error", $"Error deleting saved state: {ioEx.Message}");
                    }
                }
                // if InvalidDataEx happens: "Error loading saved state: {ex.Message}\n\nCorrupt saved state will be deleted."
                // if THAT fails with IOEx: "Error deleting saved state: {ex.Message}"

                if (data != null)
                {
                    FirstOperand = data.FirstOperand;
                    SecondOperand = data.SecondOperand;
                    Operation = data.Operation;
                }

                IsLoaded = true;
            }
        }

        public async Task SaveAsync()
        {
            var calculation = new Calculation {
                FirstOperand = _firstOperand,
                SecondOperand = _secondOperand,
                Operation = _operation,
            };

            try
            {
                await _dataManager.Save(calculation);
            }
            catch (IOException ex)
            {
                await _dialogService.ShowDialogAsync("Error", $"I/O error saving state: {ex.Message}");
            }
            catch (Exception ex)
            {
                await _dialogService.ShowDialogAsync("Error", $"Unknown error saving state: {ex.Message}");
            }
        }

        #endregion
    }
}