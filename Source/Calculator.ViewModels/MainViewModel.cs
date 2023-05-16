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
            set => SetProperty(ref _firstOperand, value);
        }

        public int SecondOperand
        {
            get => _secondOperand;
            set => SetProperty(ref _secondOperand, value);
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

        public int Result => _operation switch {
            Operation.Add => _firstOperand + _secondOperand,
            Operation.Subtract => _firstOperand - _secondOperand,
            Operation.Multiply => _firstOperand * _secondOperand,
            Operation.Divide => _secondOperand != 0 ? _firstOperand / _secondOperand : 0,
            _ => throw new NotSupportedException("Invalid operation"),
        };

        public void Calculate()
        {
            OnPropertyChanged(nameof(Result));
        }
    }
}