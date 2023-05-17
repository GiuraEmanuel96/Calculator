using Calculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private int? _firstOperand = 0;
        private int? _secondOperand = 0;
        private Operation _operation;
        private string _errorMessage = string.Empty;

        public int? FirstOperand
        {
            get => _firstOperand;
            set {
                SetProperty(ref _firstOperand, value);
            }
        }

        public int? SecondOperand
        {
            get => _secondOperand;
            set {
                SetProperty(ref _secondOperand, value);
            }
        }

        public IReadOnlyList<Operation> Operations
        {
            get {
                return new List<Operation> { Operation.Add, Operation.Subtract, Operation.Multiply, Operation.Divide };
            }
        }

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
            get
            {
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
    }
}