// UC- 10
// Helper class for Weight Unit So we can implement interface.
namespace QuantityMeasurementApp.Domain
{
    public class WeightUnitHelper : IMeasurable
    {
        private readonly WeightUnit unit;

        public WeightUnitHelper(WeightUnit unit)
        {
            this.unit = unit;
        }

        public double GetConversionFactor()
        {
            return unit switch
            {
                WeightUnit.MILLIGRAM => 0.001,
                WeightUnit.GRAM => 1.0,
                WeightUnit.KILOGRAM => 1000.0,
                WeightUnit.POUND => 453.592,
                WeightUnit.TONNE => 1000000.0,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public double ConvertToBaseUnit(double value)
        {
            Validate(value);
            return value * GetConversionFactor();
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            Validate(baseValue);
            return Math.Round(baseValue / GetConversionFactor(), 2);
        }

        public string GetUnitName()
        {
            return unit.ToString();
        }

        private void Validate(double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid measurement value.");
        }
    }
}