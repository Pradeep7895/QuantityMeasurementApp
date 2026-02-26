using System;

namespace QuantityMeasurementApp.Domain
{
    /// <summary>
    /// Length class represents a measurement with value and unit.
    /// </summary>
    public class Length
    {
        // Instance variables
        private double value;
        private LengthUnit unit;

        /// <summary>
        /// Enum to represent different length units.
        /// Conversion factors are defined relative to base unit 
        /// </summary>
        public enum LengthUnit
        {
            // 1 foot = 12 inches
            FEET = 12,
            // Base unit  
            INCHES = 1
        }

        public Length(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return value * (int)unit;
        }

        //Compare two Length objects based on converted base unit values.
        public bool Compare(Length thatLength)
        {
            if (thatLength == null)
                return false;

            return this.ConvertToBaseUnit() == thatLength.ConvertToBaseUnit();
        }

        /// <summary>
        /// Override Equals method to implement value-based equality.
        /// Follows Equality Contract rules.
        /// </summary>
        public override bool Equals(object? obj)
        {
            // Reflexive property
            if (ReferenceEquals(this, obj))
                return true;

            // Null or type check
            if (obj == null || obj.GetType() != typeof(Length))
                return false;

            // Cast and compare
            Length thatLength = (Length)obj;
            return Compare(thatLength);
        }

        /// <summary>
        /// Required when overriding Equals.
        /// </summary>
        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    }
}