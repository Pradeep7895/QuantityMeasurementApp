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

        static void Main()
        {
            Console.WriteLine("UC5: Unit-to-Unit Conversion\n");

            Console.WriteLine("1 FEET -> INCH = " + Length.convert(1.0, Length.LengthUnit.FEET, Length.LengthUnit.INCH));

            Console.WriteLine("3 YARD -> FEET = " + Length.convert(3.0, Length.LengthUnit.YARD, Length.LengthUnit.FEET));

            Console.WriteLine("36 INCH -> YARD = " + Length.convert(36.0, Length.LengthUnit.INCH, Length.LengthUnit.YARD));

            Console.WriteLine("1 CM -> INCH = " + Length.convert(1.0, Length.LengthUnit.CENTIMETERS, Length.LengthUnit.INCH));
        }
    }
}