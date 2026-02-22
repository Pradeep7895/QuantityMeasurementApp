using System;

namespace QuantityMeasurementApp.Domain
{

    // UC1: Feet Measurement Equality
    public class Feet
    {
        private readonly double value;

        public Feet(double value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            // Reflexive check
            if (ReferenceEquals(this, obj))
                return true;

            // Null and type check
            if (obj == null || GetType() != obj.GetType())
                return false;

            Feet other = (Feet)obj;

            // Floating-point safe comparison
            return this.value.CompareTo(other.value) == 0;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

    }
}