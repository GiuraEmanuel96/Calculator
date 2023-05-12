using Calculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private int _firstOperand;
        private int _secondOperand;
        private Operation _operation;

        public int FirstOperand
        {
            get => _firstOperand;
            set {
                if (SetProperty(ref _firstOperand, value))
                    OnPropertyChanged(nameof(Result));
            }
        }

        public int SecondOperand
        {
            get => _secondOperand;
            set {
                if (SetProperty(ref _secondOperand, value))
                    OnPropertyChanged(nameof(Result));
            }
        }

        public Operation Operation
        {
            get => _operation;
            set {
                if (SetProperty(ref _operation, value))
                    OnPropertyChanged(nameof(Result));
            }
        }

        public int Result => _operation switch {
            Operation.Add => _firstOperand + _secondOperand,
            Operation.Subtract => _firstOperand - _secondOperand,
            Operation.Multiply => _firstOperand * _secondOperand,
            Operation.Divide => _secondOperand != 0 ? _firstOperand / _secondOperand : 0,
            _ => throw new NotSupportedException("Invalid operation"),
        };
    }
}