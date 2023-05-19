using Calculator.Models;
using Calculator.ViewModels.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private int? _firstOperand = 0;
        private int? _secondOperand = 0;
        private Operation _operation;
        private bool _isLoaded = false;

        public MainViewModel(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            private set => SetProperty(ref _isLoaded, value);
        }

        public int? FirstOperand
        {
            get => _firstOperand;
            set => SetProperty(ref _firstOperand, value);
        }

        public int? SecondOperand
        {
            get => _secondOperand;
            set => SetProperty(ref _secondOperand, value);
        }

        public IReadOnlyList<Operation> Operations => new List<Operation> { Operation.Add, Operation.Subtract, Operation.Multiply, Operation.Divide };

        public Operation Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        public int? Result
        {
            get {
                if (_firstOperand.HasValue && _secondOperand.HasValue)
                {
                    return _operation switch {
                        Operation.Add => _firstOperand.Value + _secondOperand.Value,
                        Operation.Subtract => _firstOperand.Value - _secondOperand.Value,
                        Operation.Multiply => _firstOperand.Value * _secondOperand.Value,
                        Operation.Divide => _secondOperand.Value != 0 ? _firstOperand.Value / _secondOperand.Value : (int?)null,
                        _ => throw new NotSupportedException("Invalid operation"),
                    };
                }

                return null;
            }
        }

        public string? ErrorMessage
        {
            get {
                if (_firstOperand == null || _secondOperand == null)
                {
                    return "Neither operand can be empty.";
                }

                return null;
            }
        }

        public void Calculate()
        {
            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(ErrorMessage));
        }

        public async Task Load()
        {
            var data = await _dataManager.Load();

            if (data != null)
            {
                FirstOperand = data.FirstOperand;
                SecondOperand = data.SecondOperand;
                Operation = data.Operation;
            }

            IsLoaded = true;
        }
    }
}