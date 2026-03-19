using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Service.Services
{
    /// <summary>
    /// Main service orchestrating all quantity operations
    /// </summary>
    public class QuantityService : IQuantityService
    {
        // Dependencies for specialized operations
        private readonly IConversionService _conversionService;
        private readonly IArithmeticService _arithmeticService;
        private readonly IEqualityService _equalityService;
        private readonly IValidationService _validationService;

        /// <summary>
        /// Constructor that injects all required service dependencies
        /// </summary>
        public QuantityService(
            IConversionService conversionService,
            IArithmeticService arithmeticService,
            IEqualityService equalityService,
            IValidationService validationService)
        {
            _conversionService = conversionService;
            _arithmeticService = arithmeticService;
            _equalityService = equalityService;
            _validationService = validationService;
        }

        /// <summary>
        /// Converts a quantity from its current unit to a target unit
        /// </summary>
        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            // Validate input before processing
            if (!_validationService.IsValid(source))
                throw new ArgumentException("Invalid source quantity");

            // Delegate to conversion service
            return _conversionService.Convert(source, targetUnit);
        }

        /// <summary>
        /// Compares two quantities for equality
        /// </summary>
        /// <returns>True if quantities are equal in base unit value, false otherwise</returns>
        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate both inputs before processing
            if (!_validationService.IsValid(q1) || !_validationService.IsValid(q2))
                throw new ArgumentException("Invalid quantity input");

            // Delegate to equality service
            return _equalityService.AreEqual(q1, q2);
        }

        /// <summary>
        /// Adds two quantities of the same measurement type
        /// </summary>
        /// <returns>A new QuantityDTO with the sum in the unit of the first quantity</returns>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate that arithmetic operation is possible
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot add these quantities");

            // Delegate to arithmetic service
            return _arithmeticService.Add(q1, q2);
        }

        /// <summary>
        /// Adds two quantities and returns result in specified target unit
        /// </summary>
        /// <returns>A new QuantityDTO with the sum in the target unit</returns>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            // Validate that arithmetic operation is possible
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot add these quantities");

            // Delegate to arithmetic service with target unit
            return _arithmeticService.Add(q1, q2, targetUnit);
        }

        /// <summary>
        /// Subtracts second quantity from first quantity
        /// </summary>
        /// <returns>A new QuantityDTO with the difference in the unit of the first quantity</returns>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate that arithmetic operation is possible
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot subtract these quantities");

            // Delegate to arithmetic service
            return _arithmeticService.Subtract(q1, q2);
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in specified target unit
        /// </summary>
        /// <returns>A new QuantityDTO with the difference in the target unit</returns>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            // Validate that arithmetic operation is possible
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot subtract these quantities");

            // Delegate to arithmetic service with target unit
            return _arithmeticService.Subtract(q1, q2, targetUnit);
        }

        /// <summary>
        /// Divides first quantity by second quantity
        /// </summary>
        /// <returns>The ratio of the two quantities as a double</returns>
        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate that arithmetic operation is possible
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot divide these quantities");

            // Delegate to arithmetic service
            return _arithmeticService.Divide(q1, q2);
        }

        /// <summary>
        /// Validates a quantity DTO
        /// </summary>
        /// <returns>True if quantity is valid, false otherwise</returns>
        public bool Validate(QuantityDTO quantity)
        {
            // Delegate to validation service
            return _validationService.IsValid(quantity);
        }
    }
}