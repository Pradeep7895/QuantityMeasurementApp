using System;
using QuantityMeasurementApp.Domain;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter first value in feet:");
        string input1 = Console.ReadLine()!;

        Console.WriteLine("Enter second value in feet:");
        string input2 = Console.ReadLine()!;

        if (!double.TryParse(input1, out double value1) ||
            !double.TryParse(input2, out double value2))
        {
            Console.WriteLine("Invalid numeric input.");
            return;
        }

        var feet1 = new Feet(value1);
        var feet2 = new Feet(value2);

        bool result = feet1.Equals(feet2);

        Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
    }
}