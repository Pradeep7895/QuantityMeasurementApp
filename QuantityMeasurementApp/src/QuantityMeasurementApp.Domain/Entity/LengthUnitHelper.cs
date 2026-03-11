using System;
using QuantityMeasurementApp.Domain;

// UC- 10
// Helper class for enum length class
namespace QuantityMeasurementApp.Domain
{
    public class LengthUnitHelper : IMeasurable
    {
        private readonly LengthUnit unit;

        public LengthUnitHelper(LengthUnit unit)
        {
            this.unit = unit;
        }

        public double GetConversionFactor()
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