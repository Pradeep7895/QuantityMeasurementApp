using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Application.Menus
{
    /// <summary>
    /// ONE Generic Menu for all operations - NO dictionaries
    /// Handles all quantity operations (Convert, Add, Compare, Subtract, Divide)
    /// for any measurement type (Length, Weight, Volume, Temperature)
    /// </summary>
    public class OperationMenu : BaseMenu
    {
        // The current measurement type (Length, Weight, Volume, Temperature)
        private readonly string _measurementType;
        
        // Available units for this measurement type
        private readonly string[] _availableUnits;
        
        /// <summary>
        /// Flag indicating whether user wants to go back to main menu
        /// </summary>
        public bool IsBackSelected { get; private set; }

        /// <summary>
        /// Constructor for operation menu
        /// </summary>
        /// <param name="service">Quantity service for operations</param>
        /// <param name="measurementType">Type of measurement (Length, Weight, etc.)</param>
        /// <param name="availableUnits">Array of available units for this type</param>
        public OperationMenu(IQuantityService service, string measurementType, string[] availableUnits) 
            : base(service)
        {
            _measurementType = measurementType;
            _availableUnits = availableUnits;
            IsBackSelected = false;
        }

        /// <summary>
        /// Initializes operation menu options
        /// </summary>
        protected override void InitializeOptions()
        {
            _options.Add(new MenuOption("Convert", PerformConversion));
            _options.Add(new MenuOption("Add", PerformAddition));
            _options.Add(new MenuOption("Compare", PerformComparison));
            _options.Add(new MenuOption("Subtract", PerformSubtraction));
            _options.Add(new MenuOption("Divide", PerformDivision));
            _options.Add(new MenuOption("Back to Main Menu", () => IsBackSelected = true));
        }

        /// <summary>
        /// Performs unit conversion operation
        /// </summary>
        private void PerformConversion()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== {_measurementType} Conversion ===\n");

                // Get source quantity from user
                var source = GetQuantityInput("Enter source quantity:");
                
                // Get target unit from user
                string targetUnit = SelectUnit("Select target unit:", _availableUnits);

                // Perform conversion
                var result = _service.Convert(source, targetUnit);
                Console.WriteLine($"\nResult: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WaitForKey();
            }
        }

        /// <summary>
        /// Performs addition operation
        /// </summary>
        private void PerformAddition()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== {_measurementType} Addition ===\n");

                // Get two quantities from user
                Console.WriteLine("First Quantity:");
                var q1 = GetQuantityInput();
                Console.WriteLine("\nSecond Quantity:");
                var q2 = GetQuantityInput();

                // Ask if user wants specific target unit
                Console.Write("\nUse specific target unit? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    string targetUnit = SelectUnit("Select target unit:", _availableUnits);
                    var result = _service.Add(q1, q2, targetUnit);
                    Console.WriteLine($"\nResult: {result}");
                }
                else
                {
                    var result = _service.Add(q1, q2);
                    Console.WriteLine($"\nResult: {result}");
                }
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"\nOperation not supported: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WaitForKey();
            }
        }

        /// <summary>
        /// Performs comparison operation
        /// </summary>
        private void PerformComparison()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== {_measurementType} Comparison ===\n");

                // Get two quantities from user
                Console.WriteLine("First Quantity:");
                var q1 = GetQuantityInput();
                Console.WriteLine("\nSecond Quantity:");
                var q2 = GetQuantityInput();

                // Compare and display result
                var areEqual = _service.Compare(q1, q2);
                Console.WriteLine($"\nResult: {q1} {(areEqual ? "==" : "!=")} {q2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WaitForKey();
            }
        }

        /// <summary>
        /// Performs subtraction operation
        /// </summary>
        private void PerformSubtraction()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== {_measurementType} Subtraction ===\n");

                // Get two quantities from user
                Console.WriteLine("First Quantity (minuend):");
                var q1 = GetQuantityInput();
                Console.WriteLine("\nSecond Quantity (subtrahend):");
                var q2 = GetQuantityInput();

                // Ask if user wants specific target unit
                Console.Write("\nUse specific target unit? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    string targetUnit = SelectUnit("Select target unit:", _availableUnits);
                    var result = _service.Subtract(q1, q2, targetUnit);
                    Console.WriteLine($"\nResult: {result}");
                }
                else
                {
                    var result = _service.Subtract(q1, q2);
                    Console.WriteLine($"\nResult: {result}");
                }
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"\nOperation not supported: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WaitForKey();
            }
        }

        /// <summary>
        /// Performs division operation
        /// </summary>
        private void PerformDivision()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== {_measurementType} Division ===\n");

                // Get two quantities from user
                Console.WriteLine("First Quantity (dividend):");
                var q1 = GetQuantityInput();
                Console.WriteLine("\nSecond Quantity (divisor):");
                var q2 = GetQuantityInput();

                // Perform division and display ratio
                var result = _service.Divide(q1, q2);
                Console.WriteLine($"\nResult: {result:F4} (ratio)");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"\nOperation not supported: {ex.Message}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("\nError: Division by zero is not allowed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WaitForKey();
            }
        }

        /// <summary>
        /// Helper method to get quantity input from user
        /// </summary>
        /// <param name="prompt">Optional prompt to display</param>
        /// <returns>QuantityDTO with user input</returns>
        private QuantityDTO GetQuantityInput(string prompt = null!)
        {
            if (!string.IsNullOrEmpty(prompt))
                Console.WriteLine(prompt);

            // Get value and unit from user
            double value = ReadDouble("Enter value: ");
            string unit = SelectUnit("Select unit:", _availableUnits);

            // Create and return QuantityDTO
            return new QuantityDTO(value, unit, _measurementType);
        }

        /// <summary>
        /// Gets the menu title
        /// </summary>
        public override string GetMenuTitle() => $"{_measurementType} Measurements";
    }
}