namespace Calculator.Models;

public class Calculation
{
    public int? FirstOperand { get; set; }

    public Operation Operation { get; set; }

    public int? SecondOperand { get; set; }
}