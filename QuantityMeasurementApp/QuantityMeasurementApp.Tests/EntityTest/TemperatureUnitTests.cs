using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Model.Interfaces;
using QuantityMeasurementApp.Service.Helpers;
using QuantityMeasurementApp.Service.Services;
using System;

namespace QuantityMeasurementApp.Tests.EntityTest
{
    [TestClass]
    public class TemperatureUnitTests
    {
        // Constants
        private const double EPSILON = 0.001;
        private const double ROUNDED_EPSILON = 0.01;

        // Unit helpers
        private readonly TemperatureUnitHelper CELSIUS = new TemperatureUnitHelper(TemperatureUnit.CELSIUS);

        private readonly TemperatureUnitHelper FAHRENHEIT = new TemperatureUnitHelper(TemperatureUnit.FAHRENHEIT);

        private readonly TemperatureUnitHelper KELVIN = new TemperatureUnitHelper(TemperatureUnit.KELVIN);

        // Services
        private ConversionService _conversionService;
        private EqualityService _equalityService;

        [TestInitialize]
        public void Setup()
        {
            _conversionService = new ConversionService();
            _equalityService = new EqualityService();
        }

        //  EQUALITY TESTS 

        [TestMethod]
        public void testTemperatureEquality_CelsiusToCelsius_SameValue()
        {
            // Arrange
            var t1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToCelsius_DifferentValue_ShouldNotBeEqual()
        {
            // Arrange
            var t1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testTemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            // Arrange
            var t1 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");
            var t2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_FahrenheitToFahrenheit_DifferentValue_ShouldNotBeEqual()
        {
            // Arrange
            var t1 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");
            var t2 = new QuantityDTO(212, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testTemperatureEquality_KelvinToKelvin_SameValue()
        {
            // Arrange
            var t1 = new QuantityDTO(273.15, "KELVIN", "Temperature");
            var t2 = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
        {
            // Arrange
            var t1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
        {
            // Arrange
            var t1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(212, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
        {
            // Arrange
            var t1 = new QuantityDTO(-40, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(-40, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToKelvin_0Celsius273Point15Kelvin()
        {
            // Arrange
            var t1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_FahrenheitToKelvin_32Fahrenheit273Point15Kelvin()
        {
            // Arrange
            var t1 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");
            var t2 = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_SymmetricProperty()
        {
            // Arrange
            var t1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            bool result1 = _equalityService.AreEqual(t1, t2);
            bool result2 = _equalityService.AreEqual(t2, t1);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void testTemperatureEquality_ReflexiveProperty()
        {
            // Arrange
            var t = new QuantityDTO(50, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t, t);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testTemperatureEquality_TransitiveProperty()
        {
            // Arrange
            var a = new QuantityDTO(0, "CELSIUS", "Temperature");
            var b = new QuantityDTO(32, "FAHRENHEIT", "Temperature");
            var c = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            bool aEqualsB = _equalityService.AreEqual(a, b);
            bool bEqualsC = _equalityService.AreEqual(b, c);
            bool aEqualsC = _equalityService.AreEqual(a, c);

            // Assert
            Assert.IsTrue(aEqualsB);
            Assert.IsTrue(bEqualsC);
            Assert.IsTrue(aEqualsC);
        }

        //   CONVERSION TESTS 

        [TestMethod]
        public void testTemperatureConversion_CelsiusToFahrenheit_VariousValues()
        {
            // Arrange
            var source = new QuantityDTO(50, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(122, result.Value, EPSILON);
            Assert.AreEqual("FAHRENHEIT", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_CelsiusToFahrenheit_0()
        {
            // Arrange
            var source = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(32, result.Value, EPSILON);
            Assert.AreEqual("FAHRENHEIT", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_CelsiusToFahrenheit_100()
        {
            // Arrange
            var source = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(212, result.Value, EPSILON);
            Assert.AreEqual("FAHRENHEIT", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_FahrenheitToCelsius_VariousValues()
        {
            // Arrange
            var source = new QuantityDTO(122, "FAHRENHEIT", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "CELSIUS");

            // Assert
            Assert.AreEqual(50, result.Value, EPSILON);
            Assert.AreEqual("CELSIUS", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_FahrenheitToCelsius_32()
        {
            // Arrange
            var source = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "CELSIUS");

            // Assert
            Assert.AreEqual(0, result.Value, EPSILON);
            Assert.AreEqual("CELSIUS", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_FahrenheitToCelsius_212()
        {
            // Arrange
            var source = new QuantityDTO(212, "FAHRENHEIT", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "CELSIUS");

            // Assert
            Assert.AreEqual(100, result.Value, EPSILON);
            Assert.AreEqual("CELSIUS", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_CelsiusToKelvin()
        {
            // Arrange
            var source = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "KELVIN");

            // Assert
            Assert.AreEqual(273.15, result.Value, EPSILON);
            Assert.AreEqual("KELVIN", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_KelvinToCelsius()
        {
            // Arrange
            var source = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "CELSIUS");

            // Assert
            Assert.AreEqual(0, result.Value, EPSILON);
            Assert.AreEqual("CELSIUS", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_FahrenheitToKelvin()
        {
            // Arrange
            var source = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "KELVIN");

            // Assert
            Assert.AreEqual(273.15, result.Value, EPSILON);
            Assert.AreEqual("KELVIN", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_KelvinToFahrenheit()
        {
            // Arrange
            var source = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(32, result.Value, EPSILON);
            Assert.AreEqual("FAHRENHEIT", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_RoundTrip_PreservesValue()
        {
            // Arrange
            double original = 75;
            var source = new QuantityDTO(original, "CELSIUS", "Temperature");

            // Act
            var toFahrenheit = _conversionService.Convert(source, "FAHRENHEIT");
            var backToCelsius = _conversionService.Convert(toFahrenheit, "CELSIUS");

            // Assert
            Assert.AreEqual(original, backToCelsius.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_RoundTrip_FahrenheitToCelsiusToFahrenheit()
        {
            // Arrange
            double original = 98.6;
            var source = new QuantityDTO(original, "FAHRENHEIT", "Temperature");

            // Act
            var toCelsius = _conversionService.Convert(source, "CELSIUS");
            var backToFahrenheit = _conversionService.Convert(toCelsius, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(original, backToFahrenheit.Value, ROUNDED_EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_RoundTrip_KelvinToCelsiusToKelvin()
        {
            // Arrange
            double original = 300;
            var source = new QuantityDTO(original, "KELVIN", "Temperature");

            // Act
            var toCelsius = _conversionService.Convert(source, "CELSIUS");
            var backToKelvin = _conversionService.Convert(toCelsius, "KELVIN");

            // Assert
            Assert.AreEqual(original, backToKelvin.Value, ROUNDED_EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_SameUnit()
        {
            // Arrange
            var source = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "CELSIUS");

            // Assert
            Assert.AreEqual(100, result.Value, EPSILON);
            Assert.AreEqual("CELSIUS", result.Unit);
        }

        [TestMethod]
        public void testTemperatureConversion_ZeroValue()
        {
            // Arrange
            var source = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(32, result.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_NegativeValues()
        {
            // Arrange
            var source = new QuantityDTO(-20, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(-4, result.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_LargeValues()
        {
            // Arrange
            var source = new QuantityDTO(1000, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(1832, result.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversion_AbsoluteZero()
        {
            // Arrange - Absolute zero in Kelvin
            var kelvin = new QuantityDTO(0, "KELVIN", "Temperature");

            // Act
            var celsius = _conversionService.Convert(kelvin, "CELSIUS");
            var fahrenheit = _conversionService.Convert(kelvin, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(-273.15, celsius.Value, EPSILON);
            Assert.AreEqual(-459.67, fahrenheit.Value, ROUNDED_EPSILON);
        }

        //  CROSS CATEGORY TESTS 

        [TestMethod]
        public void testTemperatureVsLengthIncompatibility()
        {
            // Arrange
            var temp = new QuantityDTO(100, "CELSIUS", "Temperature");
            var length = new QuantityDTO(100, "FEET", "Length");

            // Act
            bool result = _equalityService.AreEqual(temp, length);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testTemperatureVsWeightIncompatibility()
        {
            // Arrange
            var temp = new QuantityDTO(50, "CELSIUS", "Temperature");
            var weight = new QuantityDTO(50, "KILOGRAM", "Weight");

            // Act
            bool result = _equalityService.AreEqual(temp, weight);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testTemperatureVsVolumeIncompatibility()
        {
            // Arrange
            var temp = new QuantityDTO(25, "CELSIUS", "Temperature");
            var volume = new QuantityDTO(25, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(temp, volume);

            // Assert
            Assert.IsFalse(result);
        }

        //  ENUM TESTS 

        [TestMethod]
        public void testTemperatureUnit_AllConstants()
        {
            Assert.IsNotNull(TemperatureUnit.CELSIUS);
            Assert.IsNotNull(TemperatureUnit.FAHRENHEIT);
            Assert.IsNotNull(TemperatureUnit.KELVIN);
        }

        [TestMethod]
        public void testTemperatureUnit_NameMethod()
        {
            Assert.AreEqual("CELSIUS", CELSIUS.GetUnitName());
            Assert.AreEqual("FAHRENHEIT", FAHRENHEIT.GetUnitName());
            Assert.AreEqual("KELVIN", KELVIN.GetUnitName());
        }

        [TestMethod]
        public void testTemperatureUnit_MeasurementType()
        {
            Assert.AreEqual("Temperature", CELSIUS.GetMeasurementType());
            Assert.AreEqual("Temperature", FAHRENHEIT.GetMeasurementType());
            Assert.AreEqual("Temperature", KELVIN.GetMeasurementType());
        }

        [TestMethod]
        public void testTemperatureUnit_ConversionFactor()
        {
            Assert.AreEqual(1.0, CELSIUS.GetConversionFactor());
            Assert.AreEqual(1.0, FAHRENHEIT.GetConversionFactor());
            Assert.AreEqual(1.0, KELVIN.GetConversionFactor());
        }

        [TestMethod]
        public void testTemperatureUnit_SupportsArithmetic()
        {
            Assert.IsFalse(CELSIUS.SupportsArithmetic());
            Assert.IsFalse(FAHRENHEIT.SupportsArithmetic());
            Assert.IsFalse(KELVIN.SupportsArithmetic());
        }

        //  VALIDATION TESTS 

        [TestMethod]
        public void testTemperatureDifferentValuesInequality()
        {
            // Arrange
            var t1 = new QuantityDTO(50, "CELSIUS", "Temperature");
            var t2 = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(t1, t2);

            // Assert
            Assert.IsFalse(result);
        }

        //  PRECISION TESTS 

        [TestMethod]
        public void testTemperatureConversionPrecision_Epsilon()
        {
            // Arrange - Body temperature
            var source = new QuantityDTO(37, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert 
            Assert.AreEqual(98.6, result.Value, ROUNDED_EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversionPrecision_BoilingPoint()
        {
            // Arrange
            var source = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert 
            Assert.AreEqual(212, result.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversionPrecision_FreezingPoint()
        {
            // Arrange
            var source = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert 
            Assert.AreEqual(32, result.Value, EPSILON);
        }

        [TestMethod]
        public void testTemperatureConversionPrecision_RoomTemperature()
        {
            // Arrange
            var source = new QuantityDTO(22, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert 
            Assert.AreEqual(71.6, result.Value, ROUNDED_EPSILON);
        }

        //  HELPER METHOD TESTS 

        [TestMethod]
        public void testTemperatureHelper_ConvertToBaseUnit()
        {
            // Test each helper's ConvertToBaseUnit method directly
            Assert.AreEqual(0, CELSIUS.ConvertToBaseUnit(0), EPSILON);
            Assert.AreEqual(100, CELSIUS.ConvertToBaseUnit(100), EPSILON);
            
            Assert.AreEqual(0, FAHRENHEIT.ConvertToBaseUnit(32), EPSILON);
            Assert.AreEqual(100, FAHRENHEIT.ConvertToBaseUnit(212), EPSILON);
            
            Assert.AreEqual(-273.15, KELVIN.ConvertToBaseUnit(0), EPSILON);
            Assert.AreEqual(0, KELVIN.ConvertToBaseUnit(273.15), EPSILON);
        }

        [TestMethod]
        public void testTemperatureHelper_ConvertFromBaseUnit()
        {
            // Test each helper's ConvertFromBaseUnit method directly
            Assert.AreEqual(0, CELSIUS.ConvertFromBaseUnit(0), EPSILON);
            Assert.AreEqual(100, CELSIUS.ConvertFromBaseUnit(100), EPSILON);
            
            Assert.AreEqual(32, FAHRENHEIT.ConvertFromBaseUnit(0), EPSILON);
            Assert.AreEqual(212, FAHRENHEIT.ConvertFromBaseUnit(100), EPSILON);
            
            Assert.AreEqual(273.15, KELVIN.ConvertFromBaseUnit(0), EPSILON);
            Assert.AreEqual(373.15, KELVIN.ConvertFromBaseUnit(100), EPSILON);
        }
    }
}