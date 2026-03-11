
//UC-11
namespace QuantityMeasurementApp.Domain
{
    /// <summary>
    /// Helper class for VolumeUnit enum.
    /// Implements IMeasurable so that VolumeUnit can work
    /// with the generic Quantity<T> class.
    /// </summary>
    public class VolumeUnitHelper : IMeasurable
    {
        private readonly VolumeUnit unit;

        public VolumeUnitHelper(VolumeUnit unit)
        {
            this.unit = unit;
        }

        /// Returns conversion factor relative to base unit (LITRE)

        public double GetConversionFactor()
        {
            return unit switch
            {
                VolumeUnit.LITRE => 1.0,
                VolumeUnit.MILLILITRE => 0.001,
                VolumeUnit.GALLON => 3.78541,
                _ => throw new ArgumentException("Unsupported volume unit")
            };
        }


        /// Converts value to base unit (LITRE)

        public double ConvertToBaseUnit(double value)
        {
            Validate(value);
            return value * GetConversionFactor();
        }


        /// Converts base unit (LITRE) value to this unit

        public double ConvertFromBaseUnit(double baseValue)
        {
            Validate(baseValue);
            return baseValue / GetConversionFactor();
        }


        /// Returns readable unit name

        public string GetUnitName()
        {
            return unit.ToString();
        }


        /// Validation for numeric values

        private void Validate(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");
        }

        public override string ToString()
        {
            return unit.ToString();
        }
    }
}