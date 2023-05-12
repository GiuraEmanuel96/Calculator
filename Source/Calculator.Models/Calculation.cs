using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models;

public class Calculation
{
    public int FirstOperand { get; set; }

    public Operation Operation { get; set; }

    public int SecondOperand { get; set; }
}