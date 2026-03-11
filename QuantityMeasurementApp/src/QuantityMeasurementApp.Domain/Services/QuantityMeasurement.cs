using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Domain
{
    ///UC - 10
    /// <summary>
    /// Demonstrates equality, conversion and addition
    /// for any measurable quantity.
    /// </summary>
    public class QuantityMeasurement
    {
        public static bool DemonstrateEquality<T>(Quantity<T> q1, Quantity<T> q2)
            where T : IMeasurable
        {
            return q1.Equals(q2);
        }

        public static Quantity<T> DemonstrateConversion<T>(Quantity<T> quantity, T targetUnit)
            where T : IMeasurable
        {
            double convertedValue = quantity.ConvertTo(targetUnit);
            return new Quantity<T>(convertedValue, targetUnit);
        }
        
        //Addition
        public static Quantity<T> DemonstrateAddition<T>(Quantity<T> q1, Quantity<T> q2)
            where T : IMeasurable
        {
            return q1.Add(q2);
        }

        public static Quantity<T> DemonstrateAddition<T>(Quantity<T> q1, Quantity<T> q2, T targetUnit)
            where T : IMeasurable
        {
            return q1.Add(q2, targetUnit);
        }

        // UC - 12
        // Subtraction
        public static Quantity<T> DemonstrateSubtraction<T>(Quantity<T> q1, Quantity<T> q2)
            where T : IMeasurable
        {
            return q1.Subtract(q2);
        }

        // Subtraction with target unit
        public static Quantity<T> DemonstrateSubtraction<T>(Quantity<T> q1, Quantity<T> q2, T targetUnit)
            where T : IMeasurable
        {
            return q1.Subtract(q2, targetUnit);
        }

        // Division
        public static double DemonstrateDivision<T>(Quantity<T> q1, Quantity<T> q2)
            where T : IMeasurable
        {
            return q1.Divide(q2);
        }
    }
}