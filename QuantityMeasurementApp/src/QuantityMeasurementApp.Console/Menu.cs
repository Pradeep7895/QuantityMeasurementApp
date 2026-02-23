using System;
using QuantityMeasurementApp.Domain;

public class Menu
{
    public void Show()
    {
        int choice;
        do
        {
            // Display menu options
            Console.WriteLine("\n===== Quantity Measurement App =====");
            Console.WriteLine("1. Compare Feet");
            Console.WriteLine("2. Compare Inches");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");


            choice = int.Parse(Console.ReadLine()!);

            // Decide what to do based on user choice
            switch (choice)
            {
                case 1:
                    CompareFeet();
                    break;

                case 2:
                    CompareInches();
                    break;

                case 0:
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        } while (choice != 0);

    }

    // Handles comparison of two Feet values
    private void CompareFeet()
    {
        // Get two valid numeric inputs from user
        if (GetValidInput("Enter first value in feet: ", out double v1) &&
            GetValidInput("Enter second value in feet: ", out double v2))
        {
            // Create two Feet objects from user input
            var feet1 = new Feet(v1);
            var feet2 = new Feet(v2);

            // Compare them using overridden Equals method
            bool result = feet1.Equals(feet2);

            // Display result
            Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
        }
    }

    // Handles comparison of two Inches values
    private void CompareInches()
    {
        // Get two valid numeric inputs from user
        if (GetValidInput("Enter first value in inches: ", out double v1) &&
            GetValidInput("Enter second value in inches: ", out double v2))
        {
            // Create two Inches objects from user input
            var inch1 = new Inches(v1);
            var inch2 = new Inches(v2);

            // Compare them using overridden Equals method
            bool result = inch1.Equals(inch2);

            Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
        }
    }

    // This method validates numeric input from the user.
    // It prevents invalid values like letters or empty input.
    // "out double value" allows the method to return the parsed number.
    private bool GetValidInput(string message, out double value)
    {
        Console.Write(message);

        // Read user input
        string? input = Console.ReadLine();

        // Try converting string input to double
        if (!double.TryParse(input, out value))
        {
            // If conversion fails, show error
            Console.WriteLine("Invalid numeric input!");
            return false;
        }

        // If conversion succeeds
        return true;
    }
}
