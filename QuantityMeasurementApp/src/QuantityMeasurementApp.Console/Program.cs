using System;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        // demonstrate length comparison
        public static void DemonstrateLengthComparison(double value1, Length.LengthUnit unit1, double value2, Length.LengthUnit unit2)
        {
            var length1 = new Length(value1, unit1);
            var length2 = new Length(value2, unit2);

            bool result = length1.Equals(length2);

            Console.WriteLine(
                $"Comparing {value1} {unit1} and {value2} {unit2} => Equal? {result}"
            );
        }

        // UC4 Main Method
        static void Main(string[] args)
        {
            // Feet and Inches comparison
            DemonstrateLengthComparison(1.0, Length.LengthUnit.FEET, 12.0, Length.LengthUnit.INCH);

            // Yards and Inches comparison
            DemonstrateLengthComparison(1.0, Length.LengthUnit.YARD, 36.0, Length.LengthUnit.INCH);

            // Centimeters and Inches comparison
            DemonstrateLengthComparison(100.0, Length.LengthUnit.CENTIMETERS, 39.3701, Length.LengthUnit.INCH);

            // Feet and Yards comparison
            DemonstrateLengthComparison(3.0, Length.LengthUnit.FEET, 1.0, Length.LengthUnit.YARD);

            // Centimeters and Feet comparison
            DemonstrateLengthComparison(30.48, Length.LengthUnit.CENTIMETERS, 1.0, Length.LengthUnit.FEET);

        }
    }
}