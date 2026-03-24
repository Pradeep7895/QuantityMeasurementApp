using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Repository.Entities;

namespace QuantityMeasurementApp.Application.Menus
{
    public class OperationsMenu
    {
        private readonly IQuantityService _service;
        private readonly string _type;
        private readonly string[] _units;

        public OperationsMenu(IQuantityService service, string type, string[] units)
        {
            _service = service;
            _type = type;
            _units = units;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\n=== {_type} OPERATIONS ===");
                Console.WriteLine("1. Convert");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Compare");
                Console.WriteLine("4. Subtract");
                Console.WriteLine("5. Divide");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Convert(); break;
                    case "2": Add(); break;
                    case "3": Compare(); break;
                    case "4": Subtract(); break;
                    case "5": Divide(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid option"); break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private QuantityDTO GetQuantity(string prompt = "")
        {
            if (!string.IsNullOrEmpty(prompt))
                Console.WriteLine(prompt);

            Console.Write("Enter value: ");
            double value = ReadDouble();
            string unit = SelectUnit();
            return new QuantityDTO(value, unit, _type);
        }

        private double ReadDouble()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                Console.Write("Invalid number. Enter again: ");
            }
        }

        private string SelectUnit()
        {
            Console.WriteLine("\nSelect unit:");
            for (int i = 0; i < _units.Length; i++)
                Console.WriteLine($"{i + 1}. {_units[i]}");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _units.Length)
                    return _units[choice - 1];
                Console.Write($"Enter 1-{_units.Length}: ");
            }
        }

        private void Convert()
        {
            Console.Clear();
            Console.WriteLine($"\n=== {_type} CONVERSION ===");
            var source = GetQuantity("Enter source quantity:");
            string targetUnit = SelectUnit();

            var result = _service.Convert(source, targetUnit);
            Console.WriteLine($"\nResult: {result.Value} {result.Unit}");
        }
        private void Add()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"\n=== {_type} ADDITION ===");

                Console.WriteLine("First quantity:");
                var q1 = GetQuantity();
                Console.WriteLine("\nSecond quantity:");
                var q2 = GetQuantity();

                Console.Write("\nUse specific target unit? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    string target = SelectUnit();
                    var result = _service.Add(q1, q2, target);
                    Console.WriteLine($"\nResult: {result.Value} {result.Unit}");
                }
                else
                {
                    var result = _service.Add(q1, q2);
                    Console.WriteLine($"\nResult: {result.Value} {result.Unit}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nUnexpected error: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private void Compare()
        {
            Console.Clear();
            Console.WriteLine($"\n=== {_type} COMPARISON ===");

            Console.WriteLine("First quantity:");
            var q1 = GetQuantity();
            Console.WriteLine("\nSecond quantity:");
            var q2 = GetQuantity();

            bool equal = _service.Compare(q1, q2);
            Console.WriteLine($"\nResult: {q1.Value} {q1.Unit} {(equal ? "==" : "!=")} {q2.Value} {q2.Unit}");
        }

        private void Subtract()
        {
            Console.Clear();
            Console.WriteLine($"\n=== {_type} SUBTRACTION ===");

            Console.WriteLine("First quantity (minuend):");
            var q1 = GetQuantity();
            Console.WriteLine("\nSecond quantity (subtrahend):");
            var q2 = GetQuantity();

            Console.Write("\nUse specific target unit? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                string target = SelectUnit();
                var result = _service.Subtract(q1, q2, target);
                Console.WriteLine($"\nResult: {result.Value} {result.Unit}");
            }
            else
            {
                var result = _service.Subtract(q1, q2);
                Console.WriteLine($"\nResult: {result.Value} {result.Unit}");
            }
        }

        private void Divide()
        {
            Console.Clear();
            Console.WriteLine($"\n=== {_type} DIVISION ===");

            Console.WriteLine("First quantity (dividend):");
            var q1 = GetQuantity();
            Console.WriteLine("\nSecond quantity (divisor):");
            var q2 = GetQuantity();

            double result = _service.Divide(q1, q2);
            Console.WriteLine($"\nResult: {result:F4} (ratio)");
        }
    }
}