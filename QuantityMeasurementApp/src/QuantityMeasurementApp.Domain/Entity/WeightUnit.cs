namespace QuantityMeasurementApp.Domain
{
    /// <summary>
    /// Standalone enum for Weight Units
    /// Base Unit = KILOGRAM
    /// </summary>
    public enum WeightUnit
    {
        MILLIGRAM,
        GRAM,
        KILOGRAM,
        POUND,
        TONNE
    }

    public static class WeightUnitExtensions
    {
        private static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.MILLIGRAM => 0.000001,
                WeightUnit.GRAM => 0.001,
                WeightUnit.KILOGRAM => 1.0,
                WeightUnit.POUND => 0.453592,
                WeightUnit.TONNE => 1000.0,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public static double convertToBaseUnit(this WeightUnit unit, double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            return value * unit.GetConversionFactor();
        }

        public static double convertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            if (!double.IsFinite(baseValue))
                throw new ArgumentException("Invalid numeric value.");

            return Math.Round(baseValue / unit.GetConversionFactor(), 6);
        }
    }
}