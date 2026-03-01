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

        /// Conversion factor is defined relative to BASE UNIT (Inches).
        public enum LengthUnit
        {
            FEET,
            INCH,
            YARD,
            CENTIMETERS
        }

        private double GetConversionFactor(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 12,
                LengthUnit.INCH => 1,
                LengthUnit.YARD => 36,
                LengthUnit.CENTIMETERS => 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public Length(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        // Converts the current length to base unit (Inches).
        private double ConvertToBaseUnit()
        {
            return value * GetConversionFactor(unit);
        }

        /// Compares two Length objects after converting to base unit
        public bool Compare(Length thatLength)
        {
            if (thatLength == null)
                return false;

            return this.ConvertToBaseUnit() == thatLength.ConvertToBaseUnit();
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
            double baseValue = ConvertToBaseUnit();
            double converted = baseValue / GetConversionFactor(targetUnit);

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

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
        public override string ToString()
        {
            return $"{value:F6} {unit}";
        }
    }
}