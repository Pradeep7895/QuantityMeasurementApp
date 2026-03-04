/// <summary> Description
/// This Use Case extends UC1 to accommodate the Equality Check for Inches
///  along with Feet. This use case is in no way trying to compare two entities,
///  Feet and Inches. They are still treated separately. Please ensure like UC1 
/// the test cases ensure complete test coverage to accurately compare and handle 
/// various edge cases
/// </summary>

namespace QuantityMeasurementApp.Domain
{
    public class Inches
    {
        // Immutable field to store the measurement value
        private readonly double value;

        // Constructor to initialize value
        public Inches(double value)
        {
            this.value = value;
        }

        // Override Equals method for value comparison
        public override bool Equals(object? obj)
        {
            //same reference
            if (ReferenceEquals(this, obj))
                return true;

            // Null check and type check
            if (obj == null || GetType() != obj.GetType())
                return false;

            Inches other = (Inches)obj;

            // Compare double values 
            return value.CompareTo(other.value) == 0;
        }

        // When overriding Equals, always override GetHashCode
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
