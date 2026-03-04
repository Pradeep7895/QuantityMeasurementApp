using System;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp
{
    public static class Program
    {
        // Equality API
        public static bool demonstrateLengthEquality(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }

        // Comparison API
        public static bool demonstrateLengthComparison(double value1, Length.LengthUnit unit1, double value2, Length.LengthUnit unit2)
        {
            var l1 = new Length(value1, unit1);
            var l2 = new Length(value2, unit2);

            return l1.Equals(l2);
        }

        // Overloaded method
        public static Length demonstrateLengthConversion(double value, Length.LengthUnit fromUnit, Length.LengthUnit toUnit)
        {
            var length = new Length(value, fromUnit);
            return length.ConvertTo(toUnit);
        }

        // Overloaded method
        public static Length demonstrateLengthConversion(Length length, Length.LengthUnit toUnit)
        {
            return length.ConvertTo(toUnit);
        }

        public static Length demonstrateLengthAddition(double value1, Length.LengthUnit unit1, double value2, Length.LengthUnit unit2)
        {
            var l1 = new Length(value1, unit1);
            var l2 = new Length(value2, unit2);

            return l1.add(l2);
        }

        static void Main()
        {
            Console.WriteLine("UC6: Addition Demo ");

            var result1 = demonstrateLengthAddition(1.0, Length.LengthUnit.FEET, 12.0, Length.LengthUnit.INCH);
            Console.WriteLine("1 FEET + 12 INCH = " + result1);

            var result2 = demonstrateLengthAddition(12.0, Length.LengthUnit.INCH, 1.0, Length.LengthUnit.FEET);
            Console.WriteLine("12 INCH + 1 FEET = " + result2);

            var result3 = demonstrateLengthAddition(1.0, Length.LengthUnit.YARD, 3.0, Length.LengthUnit.FEET);
            Console.WriteLine("1 YARD + 3 FEET = " + result3);
        }
    }
}