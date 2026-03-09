using System;

namespace QuantityMeasurementApp.Domain
{
    /// <summary>
    /// Supports Feet, Inches, Yards, Centimeters
    /// </summary>
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;

        public double Value => value;
        public LengthUnit Unit => unit;

        public Length(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        /// Compares two Length objects after converting to base unit
        public bool Compare(Length thatLength)
        {
            if (thatLength == null)
                return false;

            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = thatLength.unit.ConvertToBaseUnit(thatLength.value);

            return base1 == base2;
        }


        /// Overriding Equals method to follow equality contract.
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Length))
                return false;

            return Compare((Length)obj);
        }

        //UC-5 - UNit to Unit conversion
        public Length ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = unit.ConvertToBaseUnit(value);
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Length(converted, targetUnit);
        }

        public static double convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            var length = new Length(value, source);
            var converted = length.ConvertTo(target);

            return double.Parse(converted.ToString().Split(' ')[0]);
        }

        // UC- 6 Unit Addition
        public Length add(Length thatLength)
        {
            if (thatLength == null)
                throw new ArgumentException("Length cannot be null.");

            if (!double.IsFinite(this.value) || !double.IsFinite(thatLength.value))
                throw new ArgumentException("Invalid numeric value.");

            // Convert both to base unit
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = thatLength.unit.ConvertToBaseUnit(thatLength.value);

            // Add in base unit
            double baseSum = base1 + base2;

            // Convert back using helper method
            double resultValue = unit.ConvertFromBaseUnit(baseSum);

            return new Length(resultValue, unit);
        }

        // UC7: Add two lengths and return result in specified target unit
        public Length add(Length length, LengthUnit targetUnit)
        {
            if (length == null)
                throw new ArgumentException("Length cannot be null");

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            if (!double.IsFinite(this.value) || !double.IsFinite(length.value))
                throw new ArgumentException("Invalid numeric value");

            return addAndConvert(length, targetUnit);
        }

        // Private helper method for addition and conversion
        private Length addAndConvert(Length length, LengthUnit targetUnit)
        {
            // Convert both lengths to base unit (INCHES)
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = length.unit.ConvertToBaseUnit(length.value);

            // Add base values
            double sumBase = base1 + base2;

            // Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Length(resultValue, targetUnit);
        }

        public override int GetHashCode()
        {
            return unit.ConvertToBaseUnit(value).GetHashCode();
        }
        public override string ToString()
        {
            return $"{value:F6} {unit}";
        }
    }
}