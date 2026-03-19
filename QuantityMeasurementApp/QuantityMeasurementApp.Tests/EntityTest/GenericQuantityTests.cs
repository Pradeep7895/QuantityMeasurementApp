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
    public class GenericQuantityTests
    {
        // Constants
        private const double EPSILON = 0.0001;
        private const double ROUNDED_EPSILON = 0.01;

        // Unit helpers 
        private LengthUnitHelper FEET => new LengthUnitHelper(LengthUnit.FEET);
        private LengthUnitHelper INCH => new LengthUnitHelper(LengthUnit.INCH);
        private LengthUnitHelper YARD => new LengthUnitHelper(LengthUnit.YARD);
        private LengthUnitHelper CM => new LengthUnitHelper(LengthUnit.CENTIMETERS);

        private WeightUnitHelper KG => new WeightUnitHelper(WeightUnit.KILOGRAM);
        private WeightUnitHelper GRAM => new WeightUnitHelper(WeightUnit.GRAM);
        private WeightUnitHelper MILLIGRAM => new WeightUnitHelper(WeightUnit.MILLIGRAM);
        private WeightUnitHelper POUND => new WeightUnitHelper(WeightUnit.POUND);
        private WeightUnitHelper TONNE => new WeightUnitHelper(WeightUnit.TONNE);

        private VolumeUnitHelper LITRE => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper MILLILITRE => new VolumeUnitHelper(VolumeUnit.MILLILITRE);
        private VolumeUnitHelper GALLON => new VolumeUnitHelper(VolumeUnit.GALLON);

        private TemperatureUnitHelper CELSIUS => new TemperatureUnitHelper(TemperatureUnit.CELSIUS);
        private TemperatureUnitHelper FAHRENHEIT => new TemperatureUnitHelper(TemperatureUnit.FAHRENHEIT);
        private TemperatureUnitHelper KELVIN => new TemperatureUnitHelper(TemperatureUnit.KELVIN);

        // Services
        private ConversionService _conversionService;
        private ArithmeticService _arithmeticService;
        private EqualityService _equalityService;

        [TestInitialize]
        public void Setup()
        {
            _conversionService = new ConversionService();
            _arithmeticService = new ArithmeticService(_conversionService);
            _equalityService = new EqualityService();
        }

        //IMEASURABLE INTERFACE TESTs

        [TestMethod]
        public void testIMeasurableInterface_LengthUnitImplementation()
        {
            IMeasurable unit = new LengthUnitHelper(LengthUnit.FEET);
            Assert.IsNotNull(unit);
            Assert.AreEqual("FEET", unit.GetUnitName());
            Assert.AreEqual("Length", unit.GetMeasurementType());
            Assert.IsTrue(unit.SupportsArithmetic());
        }

        [TestMethod]
        public void testIMeasurableInterface_WeightUnitImplementation()
        {
            IMeasurable unit = new WeightUnitHelper(WeightUnit.KILOGRAM);
            Assert.IsNotNull(unit);
            Assert.AreEqual("KILOGRAM", unit.GetUnitName());
            Assert.AreEqual("Weight", unit.GetMeasurementType());
            Assert.IsTrue(unit.SupportsArithmetic());
        }

        [TestMethod]
        public void testIMeasurableInterface_VolumeUnitImplementation()
        {
            IMeasurable unit = new VolumeUnitHelper(VolumeUnit.LITRE);
            Assert.IsNotNull(unit);
            Assert.AreEqual("LITRE", unit.GetUnitName());
            Assert.AreEqual("Volume", unit.GetMeasurementType());
            Assert.IsTrue(unit.SupportsArithmetic());
        }

        [TestMethod]
        public void testIMeasurableInterface_TemperatureUnitImplementation()
        {
            IMeasurable unit = new TemperatureUnitHelper(TemperatureUnit.CELSIUS);
            Assert.IsNotNull(unit);
            Assert.AreEqual("CELSIUS", unit.GetUnitName());
            Assert.AreEqual("Temperature", unit.GetMeasurementType());
            Assert.IsFalse(unit.SupportsArithmetic());
        }

        [TestMethod]
        public void testIMeasurableInterface_ConsistentBehavior()
        {
            IMeasurable l = new LengthUnitHelper(LengthUnit.FEET);
            IMeasurable w = new WeightUnitHelper(WeightUnit.KILOGRAM);
            IMeasurable v = new VolumeUnitHelper(VolumeUnit.LITRE);
            IMeasurable t = new TemperatureUnitHelper(TemperatureUnit.CELSIUS);

            Assert.IsTrue(l.GetConversionFactor() > 0);
            Assert.IsTrue(w.GetConversionFactor() > 0);
            Assert.IsTrue(v.GetConversionFactor() > 0);
            Assert.IsTrue(t.GetConversionFactor() == 1.0);
        }

        // EQUALITY TEST 

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Equality()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Equality()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(1000, "GRAM", "Weight");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testGenericQuantity_VolumeOperations_Equality()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITRE", "Volume");
            var q2 = new QuantityDTO(1000, "MILLILITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testGenericQuantity_TemperatureOperations_Equality()
        {
            // Arrange
            var q1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testCrossCategoryPrevention_LengthVsWeight()
        {
            // Arrange
            var length = new QuantityDTO(1, "FEET", "Length");
            var weight = new QuantityDTO(1, "KILOGRAM", "Weight");

            // Act
            bool result = _equalityService.AreEqual(length, weight);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testCrossCategoryPrevention_LengthVsVolume()
        {
            // Arrange
            var length = new QuantityDTO(1, "FEET", "Length");
            var volume = new QuantityDTO(1, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(length, volume);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testCrossCategoryPrevention_WeightVsTemperature()
        {
            // Arrange
            var weight = new QuantityDTO(1, "KILOGRAM", "Weight");
            var temp = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(weight, temp);

            // Assert
            Assert.IsFalse(result);
        }

        //CONVERSION TESTS 

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Conversion()
        {
            // Arrange
            var source = new QuantityDTO(1, "FEET", "Length");

            // Act
            var result = _conversionService.Convert(source, "INCH");

            // Assert
            Assert.AreEqual(12, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("INCH", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Conversion()
        {
            // Arrange
            var source = new QuantityDTO(1, "KILOGRAM", "Weight");

            // Act
            var result = _conversionService.Convert(source, "GRAM");

            // Assert
            Assert.AreEqual(1000, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("GRAM", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_VolumeOperations_Conversion()
        {
            // Arrange
            var source = new QuantityDTO(1, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(1000, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_TemperatureOperations_Conversion()
        {
            // Arrange
            var source = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _conversionService.Convert(source, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(32, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("FAHRENHEIT", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_Conversion_AllUnitCombinations()
        {
            // Arrange
            var source = new QuantityDTO(2, "FEET", "Length");

            // Act
            var toInches = _conversionService.Convert(source, "INCH");
            var toYards = _conversionService.Convert(source, "YARD");
            var toCm = _conversionService.Convert(source, "CENTIMETERS");

            // Assert
            Assert.AreEqual(24, toInches.Value, ROUNDED_EPSILON);
            Assert.AreEqual(0.67, toYards.Value, ROUNDED_EPSILON);
            Assert.AreEqual(60.96, toCm.Value, ROUNDED_EPSILON);
        }

        [TestMethod]
        public void testConversion_RoundTrip_PreservesValue()
        {
            // Arrange
            double originalValue = 5.0;
            var source = new QuantityDTO(originalValue, "FEET", "Length");

            // Act
            var toInches = _conversionService.Convert(source, "INCH");
            var backToFeet = _conversionService.Convert(toInches, "FEET");

            // Assert
            Assert.AreEqual(originalValue, backToFeet.Value, ROUNDED_EPSILON);
        }

        // ADDITION TESTS 

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Addition()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Addition()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(1000, "GRAM", "Weight");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("KILOGRAM", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_VolumeOperations_Addition()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITRE", "Volume");
            var q2 = new QuantityDTO(500, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(1.5, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_Addition_TargetUnit()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(1000, "GRAM", "Weight");

            // Act
            var result = _arithmeticService.Add(q1, q2, "GRAM");

            // Assert
            Assert.AreEqual(2000, result.Value, ROUNDED_EPSILON);
            Assert.AreEqual("GRAM", result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_Addition_AllUnitCombinations()
        {
            // Arrange
            var q1 = new QuantityDTO(3, "FEET", "Length");
            var q2 = new QuantityDTO(1, "YARD", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.IsTrue(result.Value > 0);
            Assert.AreEqual(6, result.Value, ROUNDED_EPSILON); // 3 feet + 3 feet = 6 feet
        }

        //BACKWARD COMPATIBILITY TESTS 

        [TestMethod]
        public void testBackwardCompatibility_AllUC1Through9Tests()
        {
            // Arrange
            var q = new QuantityDTO(1, "FEET", "Length");

            // Assert
            Assert.AreEqual(1, q.Value);
            Assert.AreEqual("FEET", q.Unit);
            Assert.AreEqual("Length", q.MeasurementType);
        }

        // HASHCODE TESTS 

        [TestMethod]
        public void testHashCode_GenericQuantity_Consistency()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(1, "FEET", "Length");

            // Act - Convert to base values and compare
            double base1 = _conversionService.ToBaseUnit(q1);
            double base2 = _conversionService.ToBaseUnit(q2);

            // Assert 
            Assert.AreEqual(base1.GetHashCode(), base2.GetHashCode());
        }

        [TestMethod]
        public void testEquals_GenericQuantity_ContractPreservation()
        {
            // Arrange
            var q = new QuantityDTO(1, "FEET", "Length");

            // Act & Assert - Reflexive property
            Assert.IsTrue(_equalityService.AreEqual(q, q));
        }

        [TestMethod]
        public void testEquals_Symmetric_Property()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            bool result1 = _equalityService.AreEqual(q1, q2);
            bool result2 = _equalityService.AreEqual(q2, q1);

            // Assert
            Assert.AreEqual(result1, result2);
            Assert.IsTrue(result1);
        }

        //ENUM BEHAVIOR TESTS

        [TestMethod]
        public void testEnumAsUnitCarrier_BehaviorEncapsulation()
        {
            // Arrange
            var f = new LengthUnitHelper(LengthUnit.FEET);

            // Assert
            Assert.AreEqual("FEET", f.GetUnitName());
            Assert.AreEqual("Length", f.GetMeasurementType());
            Assert.AreEqual(12.0, f.GetConversionFactor());
        }

        [TestMethod]
        public void testAllLengthUnits_HaveValidConversionFactors()
        {
            // Assert
            Assert.AreEqual(12.0, FEET.GetConversionFactor());
            Assert.AreEqual(1.0, INCH.GetConversionFactor());
            Assert.AreEqual(36.0, YARD.GetConversionFactor());
            Assert.AreEqual(0.393701, CM.GetConversionFactor(), EPSILON);
        }

        [TestMethod]
        public void testAllWeightUnits_HaveValidConversionFactors()
        {
            // Assert
            Assert.AreEqual(0.001, MILLIGRAM.GetConversionFactor(), EPSILON);
            Assert.AreEqual(1.0, GRAM.GetConversionFactor());
            Assert.AreEqual(1000.0, KG.GetConversionFactor());
            Assert.AreEqual(453.592, POUND.GetConversionFactor(), EPSILON);
            Assert.AreEqual(1000000.0, TONNE.GetConversionFactor());
        }

        [TestMethod]
        public void testAllVolumeUnits_HaveValidConversionFactors()
        {
            // Assert
            Assert.AreEqual(1.0, LITRE.GetConversionFactor());
            Assert.AreEqual(0.001, MILLILITRE.GetConversionFactor(), EPSILON);
            Assert.AreEqual(3.78541, GALLON.GetConversionFactor(), EPSILON);
        }

        // IMMUTABILITY TESTS 

        [TestMethod]
        public void testImmutability_GenericQuantity()
        {
            // Arrange
            var q = new QuantityDTO(1, "FEET", "Length");

            // Act - Perform operations that return new objects
            var converted = _conversionService.Convert(q, "INCH");
            var added = _arithmeticService.Add(q, new QuantityDTO(1, "FEET", "Length"));

            // Assert 
            Assert.AreEqual(1, q.Value);
            Assert.AreEqual("FEET", q.Unit);
            
            Assert.AreEqual(12, converted.Value);
            Assert.AreEqual(2, added.Value);
        }

    }
}