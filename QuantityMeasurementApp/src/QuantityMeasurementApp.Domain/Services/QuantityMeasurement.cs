using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Domain
{
    public class QuantityMeasurement
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

        // Demonstrate addition with explicit target unit
        public static Length demonstrateLengthAddition(Length length1, Length length2, Length.LengthUnit targetUnit)
        {
            if (length1 == null || length2 == null)
                throw new ArgumentException("Length arguments cannot be null");

            return length1.add(length2, targetUnit);
        }
}
}
