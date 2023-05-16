using Calculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _firstOperand = "0";
        private string _secondOperand = "0";
        private Operation _operation;

        public string FirstOperand
        {
            get => _firstOperand;
            set {
                if (int.TryParse(value, out int number))
                {
                    SetProperty(ref _firstOperand, value);
                }
            }
        }

        public string SecondOperand
        {
            get => _secondOperand;
            set {
                if (int.TryParse(value, out int number))
                {
                    SetProperty(ref _secondOperand, value);
                }
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

        public int Result => _operation switch {
            Operation.Add => int.Parse(_firstOperand) + int.Parse(_secondOperand),
            Operation.Subtract => int.Parse(_firstOperand) - int.Parse(_secondOperand),
            Operation.Multiply => int.Parse(_firstOperand) * int.Parse(_secondOperand),
            Operation.Divide => int.Parse(_secondOperand) != 0 ? int.Parse(_firstOperand) / int.Parse(_secondOperand) : 0,
            _ => throw new NotSupportedException("Invalid operation"),
        };

        public void Calculate()
        {
            OnPropertyChanged(nameof(Result));
        }
    }
}