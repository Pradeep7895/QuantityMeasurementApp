
namespace QuantityMeasurementApp.Domain
{
    // UC8: Standalone enum
    public enum LengthUnit
    {
        FEET,
        INCH,
        YARD,
        CENTIMETERS
    }

     // UC8: Conversion responsibility moved here
    public static class LengthUnitExtension
    {
        // Base unit = INCHES 
        private static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 12.0,
                LengthUnit.INCH => 1.0,
                LengthUnit.YARD => 36.0,
                LengthUnit.CENTIMETERS => 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

          // Convert value from this unit → base unit (INCHES)
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            Validate(value);
            return value * unit.GetConversionFactor();
        }
         // Convert base unit (INCHES) → this unit
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            Validate(baseValue);
            return baseValue / unit.GetConversionFactor();
        }

        private static void Validate(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number.");
        }
    }
}