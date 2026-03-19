using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Service.Interfaces
{
    /// <summary>
    /// Main service interface for quantity operations
    /// </summary>
    public interface IQuantityService
    {
        QuantityDTO Convert(QuantityDTO source, string targetUnit);
        bool Compare(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        double Divide(QuantityDTO q1, QuantityDTO q2);
        bool Validate(QuantityDTO quantity);
    }
}