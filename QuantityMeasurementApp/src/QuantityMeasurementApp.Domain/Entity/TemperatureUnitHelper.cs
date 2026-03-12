// UC- 14
// Temperature helper class 
namespace QuantityMeasurementApp.Domain
{


    public class TemperatureUnitHelper : IMeasurable
    {
        private readonly TemperatureUnit unit;

        public TemperatureUnitHelper(TemperatureUnit unit)
        {
            this.unit = unit;
        }

        public string GetUnitName()
        {
            return unit.ToString();
        }

        public double GetConversionFactor()
        {
            return 1.0;
        }

        public double ConvertToBaseUnit(double value)
        {
            // Base unit = Celsius
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return value;

                case TemperatureUnit.FAHRENHEIT:
                    return (value - 32) * 5 / 9;

                case TemperatureUnit.KELVIN:
                    return value - 273.15;    

                default:
                    throw new InvalidOperationException("Unsupported temperature unit");
            }
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return baseValue;

                case TemperatureUnit.FAHRENHEIT:
                    return (baseValue * 9 / 5) + 32;

                case TemperatureUnit.KELVIN:
                    return baseValue + 273.15;    

                default:
                    throw new InvalidOperationException("Unsupported temperature unit");
            }
        }

        // Temperature does NOT support arithmetic
        public bool SupportsArithmetic()
        {
            return false;
        }

        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException(
                $"Temperature does not support {operation} operations.");
        }
    }
}