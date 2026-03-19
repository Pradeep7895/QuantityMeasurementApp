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
    public class VolumeQuantityTests
    {
        // Constants
        private const double EPS = 1e-3;
        private const double ROUNDED_EPS = 0.01;

        // Unit helpers (kept for reference and helper tests)
        private VolumeUnitHelper L => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper ML => new VolumeUnitHelper(VolumeUnit.MILLILITRE);
        private VolumeUnitHelper GAL => new VolumeUnitHelper(VolumeUnit.GALLON);

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

        //  EQUALITY TESTS 

        [TestMethod]
        public void testEquality_LitreToLitre_SameValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_LitreToLitre_DifferentValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(2.0, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testEquality_LitreToMillilitre_EquivalentValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_MillilitreToLitre_EquivalentValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");
            var q2 = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_LitreToGallon_EquivalentValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(0.264172, "GALLON", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_GallonToLitre_EquivalentValue()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "GALLON", "Volume");
            var q2 = new QuantityDTO(3.78541, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_VolumeVsLength_Incompatible()
        {
            // Arrange
            var volume = new QuantityDTO(1.0, "LITRE", "Volume");
            var length = new QuantityDTO(1.0, "FEET", "Length");

            // Act
            bool result = _equalityService.AreEqual(volume, length);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testEquality_VolumeVsWeight_Incompatible()
        {
            // Arrange
            var volume = new QuantityDTO(1.0, "LITRE", "Volume");
            var weight = new QuantityDTO(1.0, "KILOGRAM", "Weight");

            // Act
            bool result = _equalityService.AreEqual(volume, weight);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testEquality_VolumeVsTemperature_Incompatible()
        {
            // Arrange
            var volume = new QuantityDTO(1.0, "LITRE", "Volume");
            var temp = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            bool result = _equalityService.AreEqual(volume, temp);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            // Arrange
            var q = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(q, q);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_TransitiveProperty()
        {
            // Arrange
            var a = new QuantityDTO(1.0, "LITRE", "Volume");
            var b = new QuantityDTO(1000.0, "MILLILITRE", "Volume");
            var c = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            bool aEqualsB = _equalityService.AreEqual(a, b);
            bool bEqualsC = _equalityService.AreEqual(b, c);
            bool aEqualsC = _equalityService.AreEqual(a, c);

            // Assert
            Assert.IsTrue(aEqualsB);
            Assert.IsTrue(bEqualsC);
            Assert.IsTrue(aEqualsC);
        }

        [TestMethod]
        public void testEquality_SymmetricProperty()
        {
            // Arrange
            var a = new QuantityDTO(1.0, "LITRE", "Volume");
            var b = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            bool result1 = _equalityService.AreEqual(a, b);
            bool result2 = _equalityService.AreEqual(b, a);

            // Assert
            Assert.AreEqual(result1, result2);
            Assert.IsTrue(result1);
        }

        [TestMethod]
        public void testEquality_ZeroValue()
        {
            // Arrange
            var a = new QuantityDTO(0.0, "LITRE", "Volume");
            var b = new QuantityDTO(0.0, "MILLILITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(a, b);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_NegativeVolume()
        {
            // Arrange
            var a = new QuantityDTO(-1.0, "LITRE", "Volume");
            var b = new QuantityDTO(-1000.0, "MILLILITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(a, b);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_LargeVolumeValue()
        {
            // Arrange
            var a = new QuantityDTO(1000000.0, "MILLILITRE", "Volume");
            var b = new QuantityDTO(1000.0, "LITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(a, b);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_SmallVolumeValue()
        {
            // Arrange
            var a = new QuantityDTO(0.001, "LITRE", "Volume");
            var b = new QuantityDTO(1.0, "MILLILITRE", "Volume");

            // Act
            bool result = _equalityService.AreEqual(a, b);

            // Assert
            Assert.IsTrue(result);
        }

        //  CONVERSION TESTS 

        [TestMethod]
        public void testConversion_LitreToMillilitre()
        {
            // Arrange
            var source = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(1000.0, result.Value, EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testConversion_MillilitreToLitre()
        {
            // Arrange
            var source = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "LITRE");

            // Assert
            Assert.AreEqual(1.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testConversion_LitreToGallon()
        {
            // Arrange
            var source = new QuantityDTO(3.78541, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "GALLON");

            // Assert
            Assert.AreEqual(1.0, result.Value, EPS);
            Assert.AreEqual("GALLON", result.Unit);
        }

        [TestMethod]
        public void testConversion_GallonToMillilitre()
        {
            // Arrange
            var source = new QuantityDTO(1.0, "GALLON", "Volume");

            // Act
            var result = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(3785.41, result.Value, ROUNDED_EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testConversion_SameUnit()
        {
            // Arrange
            var source = new QuantityDTO(5.0, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "LITRE");

            // Assert
            Assert.AreEqual(5.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            // Arrange
            var source = new QuantityDTO(0.0, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(0.0, result.Value, EPS);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            // Arrange
            var source = new QuantityDTO(-1.0, "LITRE", "Volume");

            // Act
            var result = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(-1000.0, result.Value, EPS);
        }

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            // Arrange
            double original = 1.5;
            var source = new QuantityDTO(original, "LITRE", "Volume");

            // Act
            var toMl = _conversionService.Convert(source, "MILLILITRE");
            var backToL = _conversionService.Convert(toMl, "LITRE");

            // Assert
            Assert.AreEqual(original, backToL.Value, EPS);
        }

        [TestMethod]
        public void testConversion_RoundTrip_GallonToLitreToGallon()
        {
            // Arrange
            double original = 2.5;
            var source = new QuantityDTO(original, "GALLON", "Volume");

            // Act
            var toLitre = _conversionService.Convert(source, "LITRE");
            var backToGallon = _conversionService.Convert(toLitre, "GALLON");

            // Assert
            Assert.AreEqual(original, backToGallon.Value, ROUNDED_EPS);
        }

        //  ADDITION TESTS 

        [TestMethod]
        public void testAddition_SameUnit_LitrePlusLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(2.0, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(3.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_SameUnit_MillilitrePlusMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(500.0, "MILLILITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(1000.0, result.Value, EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_SameUnit_GallonPlusGallon()
        {
            // Arrange
            var q1 = new QuantityDTO(1.5, "GALLON", "Volume");
            var q2 = new QuantityDTO(2.5, "GALLON", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(4.0, result.Value, EPS);
            Assert.AreEqual("GALLON", result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_MillilitrePlusLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");
            var q2 = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2000.0, result.Value, EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_GallonPlusLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "GALLON", "Volume");
            var q2 = new QuantityDTO(3.78541, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2.0, result.Value, EPS);
            Assert.AreEqual("GALLON", result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_LitrePlusGallon()
        {
            // Arrange
            var q1 = new QuantityDTO(3.78541, "LITRE", "Volume");
            var q2 = new QuantityDTO(1.0, "GALLON", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(7.57082, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Litre()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2, "LITRE");

            // Assert
            Assert.AreEqual(2.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2, "MILLILITRE");

            // Assert
            Assert.AreEqual(2000.0, result.Value, EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Gallon()
        {
            // Arrange
            var q1 = new QuantityDTO(3.78541, "LITRE", "Volume");
            var q2 = new QuantityDTO(3.78541, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(q1, q2, "GALLON");

            // Assert
            Assert.AreEqual(2.0, result.Value, ROUNDED_EPS);
            Assert.AreEqual("GALLON", result.Unit);
        }

        [TestMethod]
        public void testAddition_WithZero()
        {
            // Arrange
            var a = new QuantityDTO(5.0, "LITRE", "Volume");
            var b = new QuantityDTO(0.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(a, b);

            // Assert
            Assert.AreEqual(5.0, result.Value, EPS);
        }

        [TestMethod]
        public void testAddition_NegativeValues()
        {
            // Arrange
            var a = new QuantityDTO(5.0, "LITRE", "Volume");
            var b = new QuantityDTO(-2000.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Add(a, b);

            // Assert
            Assert.AreEqual(3.0, result.Value, EPS);
        }

        [TestMethod]
        public void testAddition_Commutativity()
        {
            // Arrange
            var a = new QuantityDTO(1.0, "LITRE", "Volume");
            var b = new QuantityDTO(1000.0, "MILLILITRE", "Volume");

            // Act
            var result1 = _arithmeticService.Add(a, b);
            var result2 = _arithmeticService.Add(b, a);

            // Assert - Convert both to same unit for comparison
            var result2InLitre = _conversionService.Convert(result2, "LITRE");
            
            Assert.AreEqual(result1.Value, result2InLitre.Value, EPS);
        }

        // SUBTRACTION TESTS 

        [TestMethod]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(5.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(3.0, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(2.0, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_LitreMinusMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(2.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(1.5, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(2.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "MILLILITRE");

            // Assert
            Assert.AreEqual(1500.0, result.Value, EPS);
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        //  DIVISION TESTS 

        [TestMethod]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(10.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(2.0, "LITRE", "Volume");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5.0, result, EPS);
        }

        [TestMethod]
        public void testDivision_CrossUnit_LitreDividedByMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(2.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(4.0, result, EPS); // 2000mL / 500mL = 4
        }

        [TestMethod]
        public void testDivision_CrossUnit_MillilitreDividedByLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(500.0, "MILLILITRE", "Volume");
            var q2 = new QuantityDTO(1.0, "LITRE", "Volume");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(0.5, result, EPS); // 500mL / 1000mL = 0.5
        }

        //  ENUM / HELPER TESTS 

        [TestMethod]
        public void testVolumeUnitEnum_LitreConstant()
        {
            Assert.AreEqual(1.0, L.GetConversionFactor(), EPS);
            Assert.AreEqual("LITRE", L.GetUnitName());
            Assert.AreEqual("Volume", L.GetMeasurementType());
            Assert.IsTrue(L.SupportsArithmetic());
        }

        [TestMethod]
        public void testVolumeUnitEnum_MillilitreConstant()
        {
            Assert.AreEqual(0.001, ML.GetConversionFactor(), EPS);
            Assert.AreEqual("MILLILITRE", ML.GetUnitName());
            Assert.AreEqual("Volume", ML.GetMeasurementType());
            Assert.IsTrue(ML.SupportsArithmetic());
        }

        [TestMethod]
        public void testVolumeUnitEnum_GallonConstant()
        {
            Assert.AreEqual(3.78541, GAL.GetConversionFactor(), EPS);
            Assert.AreEqual("GALLON", GAL.GetUnitName());
            Assert.AreEqual("Volume", GAL.GetMeasurementType());
            Assert.IsTrue(GAL.SupportsArithmetic());
        }

        [TestMethod]
        public void testVolumeHelper_ConvertToBaseUnit()
        {
            Assert.AreEqual(1.0, L.ConvertToBaseUnit(1.0), EPS);
            Assert.AreEqual(1.0, ML.ConvertToBaseUnit(1000.0), EPS);
            Assert.AreEqual(3.78541, GAL.ConvertToBaseUnit(1.0), EPS);
        }

        [TestMethod]
        public void testVolumeHelper_ConvertFromBaseUnit()
        {
            Assert.AreEqual(1.0, L.ConvertFromBaseUnit(1.0), EPS);
            Assert.AreEqual(1000.0, ML.ConvertFromBaseUnit(1.0), EPS);
            Assert.AreEqual(1.0, GAL.ConvertFromBaseUnit(3.78541), EPS);
        }

        // ============ ARCHITECTURE TESTS ============

        [TestMethod]
        public void testGenericQuantity_VolumeOperations_Consistency()
        {
            // Arrange
            var q = new QuantityDTO(1.0, "LITRE", "Volume");

            // Assert
            Assert.IsNotNull(q);
            Assert.AreEqual(1.0, q.Value);
            Assert.AreEqual("LITRE", q.Unit);
            Assert.AreEqual("Volume", q.MeasurementType);
        }

        [TestMethod]
        public void testScalability_VolumeIntegration()
        {
            // Arrange
            var source = new QuantityDTO(2.0, "LITRE", "Volume");

            // Act
            var converted = _conversionService.Convert(source, "MILLILITRE");

            // Assert
            Assert.AreEqual(2000.0, converted.Value, EPS);
        }

        [TestMethod]
        public void testVolume_AllOperations_WithDifferentUnits()
        {
            // Arrange
            var q1 = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act - Test multiple operations
            var added = _arithmeticService.Add(q1, q2);
            var subtracted = _arithmeticService.Subtract(q1, q2);
            var divided = _arithmeticService.Divide(q1, q2);
            var areEqual = _equalityService.AreEqual(
                new QuantityDTO(1.5, "LITRE", "Volume"), 
                added);

            // Assert
            Assert.AreEqual(1.5, added.Value, EPS);
            Assert.AreEqual(0.5, subtracted.Value, EPS);
            Assert.AreEqual(2.0, divided, EPS);
            Assert.IsTrue(areEqual);
        }

        // ============ IMMUTABILITY TESTS ============

        [TestMethod]
        public void testImmutability_AfterOperations()
        {
            // Arrange
            var original = new QuantityDTO(1.0, "LITRE", "Volume");
            var q2 = new QuantityDTO(500.0, "MILLILITRE", "Volume");

            // Act
            var added = _arithmeticService.Add(original, q2);
            var converted = _conversionService.Convert(original, "MILLILITRE");

            // Assert - Original unchanged
            Assert.AreEqual(1.0, original.Value);
            Assert.AreEqual("LITRE", original.Unit);
            
            Assert.AreEqual(1.5, added.Value);
            Assert.AreEqual(1000.0, converted.Value);
        }
    }
}