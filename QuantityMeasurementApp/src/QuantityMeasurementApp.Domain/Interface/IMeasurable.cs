
namespace QuantityMeasurementApp.Domain
{
    public interface IMeasurable
    {
        double GetConversionFactor();

        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();

        // Default arithmetic support
        bool SupportsArithmetic()
        {
            return true;
        }

        // Validate operation support
        void ValidateOperationSupport(string operation)
        {
            
        }
    }
}