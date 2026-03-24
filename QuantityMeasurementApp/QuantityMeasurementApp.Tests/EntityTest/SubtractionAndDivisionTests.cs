using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Helpers;
using QuantityMeasurementApp.Service.Services;
using System;

namespace QuantityMeasurementApp.Tests.EntityTest
{
    [TestClass]
    public class SubtractionAndDivisionTests
    {
        // Constants
        private const double EPS = 0.01;

        // Unit helpers (kept for reference)
        private LengthUnitHelper FEET => new LengthUnitHelper(LengthUnit.FEET);
        private LengthUnitHelper INCH => new LengthUnitHelper(LengthUnit.INCH);
        private WeightUnitHelper KG => new WeightUnitHelper(WeightUnit.KILOGRAM);
        private WeightUnitHelper GRAM => new WeightUnitHelper(WeightUnit.GRAM);
        private VolumeUnitHelper LITRE => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper ML => new VolumeUnitHelper(VolumeUnit.MILLILITRE);

        // Services
        private ConversionService _conversionService;
        private ArithmeticService _arithmeticService;

        [TestInitialize]
        public void Setup()
        {
            _conversionService = new ConversionService();
            _arithmeticService = new ArithmeticService(_conversionService);
        }

        // SUBTRACTION TESTS 

        [TestMethod]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value, EPS);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "LITRE", "Volume");
            var q2 = new QuantityDTO(3, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(7, result.Value, EPS);
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_SameUnit_KilogramMinusKilogram()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(4, "KILOGRAM", "Weight");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(6, result.Value, EPS);
            Assert.AreEqual("KILOGRAM", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(9.5, result.Value, EPS); // 10 feet - 0.5 feet = 9.5 feet
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_InchesMinusFeet()
        {
            // Arrange
            var q1 = new QuantityDTO(120, "INCH", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(60, result.Value, EPS); // 120 inches - 60 inches = 60 inches
            Assert.AreEqual("INCH", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_KilogramMinusGram()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(500, "GRAM", "Weight");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(1.5, result.Value, EPS); // 2 kg - 0.5 kg = 1.5 kg
            Assert.AreEqual("KILOGRAM", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_LitreMinusMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(3, "LITRE", "Volume");
            var q2 = new QuantityDTO(500, "MILLILITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(2.5, result.Value, EPS); // 3 L - 0.5 L = 2.5 L
            Assert.AreEqual("LITRE", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Feet()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "FEET");

            // Assert
            Assert.AreEqual(9.5, result.Value, EPS);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Inches()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "INCH");

            // Assert
            Assert.AreEqual(114, result.Value, EPS); // 120 inches - 6 inches = 114 inches
            Assert.AreEqual("INCH", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Yards()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "YARD");

            // Assert
            Assert.AreEqual(3.17, result.Value, EPS); // 9.5 feet = 3.17 yards
            Assert.AreEqual("YARD", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "LITRE", "Volume");
            var q2 = new QuantityDTO(2, "LITRE", "Volume");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "MILLILITRE");

            // Assert
            Assert.AreEqual(3000, result.Value, EPS); // 3 L = 3000 mL
            Assert.AreEqual("MILLILITRE", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Gram()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(500, "GRAM", "Weight");

            // Act
            var result = _arithmeticService.Subtract(q1, q2, "GRAM");

            // Assert
            Assert.AreEqual(1500, result.Value, EPS); // 2000 g - 500 g = 1500 g
            Assert.AreEqual("GRAM", result.Unit);
        }

        [TestMethod]
        public void testSubtraction_ResultingInNegative()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(10, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(-5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ResultingInZero()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(120, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(0, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithZeroOperand()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(0, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithNegativeValues()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(-2, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(7, result.Value, EPS); // 5 - (-2) = 7
        }

        [TestMethod]
        public void testSubtraction_NonCommutative()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act
            var aMinusB = _arithmeticService.Subtract(a, b);
            var bMinusA = _arithmeticService.Subtract(b, a);

            // Assert
            Assert.AreEqual(5, aMinusB.Value, EPS);
            Assert.AreEqual(-5, bMinusA.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithLargeValues()
        {
            // Arrange
            var q1 = new QuantityDTO(1000000, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(500000, "KILOGRAM", "Weight");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(500000, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithSmallValues()
        {
            // Arrange
            var q1 = new QuantityDTO(0.001, "FEET", "Length");
            var q2 = new QuantityDTO(0.0005, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(0.0005, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ChainedOperations()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");
            var q3 = new QuantityDTO(1, "FEET", "Length");

            // Act: (10 - 2) - 1 = 7
            var step1 = _arithmeticService.Subtract(q1, q2);
            var result = _arithmeticService.Subtract(step1, q3);

            // Assert
            Assert.AreEqual(7, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ChainedOperations_WithDifferentUnits()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");  // 1 foot
            var q3 = new QuantityDTO(6, "INCH", "Length");   // 0.5 foot

            // Act: (10 - 1) - 0.5 = 8.5 feet
            var step1 = _arithmeticService.Subtract(q1, q2);
            var result = _arithmeticService.Subtract(step1, q3);

            // Assert
            Assert.AreEqual(8.5, result.Value, EPS);
        }

        //  DIVISION TESTS

        [TestMethod]
        public void testDivision_SameUnit_FeetDividedByFeet()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result, EPS);
        }

        [TestMethod]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "LITRE", "Volume");
            var q2 = new QuantityDTO(5, "LITRE", "Volume");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(2, result, EPS);
        }

        [TestMethod]
        public void testDivision_SameUnit_KilogramDividedByKilogram()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(2, "KILOGRAM", "Weight");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result, EPS);
        }

        [TestMethod]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            // Arrange
            var q1 = new QuantityDTO(24, "INCH", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1, result, EPS); // 24 inches / 24 inches = 1
        }

        [TestMethod]
        public void testDivision_CrossUnit_InchesDividedByFeet()
        {
            // Arrange
            var q1 = new QuantityDTO(36, "INCH", "Length");
            var q2 = new QuantityDTO(1, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(3, result, EPS); // 36 inches / 12 inches = 3
        }

        [TestMethod]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(2000, "GRAM", "Weight");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1, result, EPS); // 2000g / 2000g = 1
        }

        [TestMethod]
        public void testDivision_CrossUnit_GramDividedByKilogram()
        {
            // Arrange
            var q1 = new QuantityDTO(500, "GRAM", "Weight");
            var q2 = new QuantityDTO(1, "KILOGRAM", "Weight");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(0.5, result, EPS); // 500g / 1000g = 0.5
        }

        [TestMethod]
        public void testDivision_CrossUnit_LitreDividedByMillilitre()
        {
            // Arrange
            var q1 = new QuantityDTO(3, "LITRE", "Volume");
            var q2 = new QuantityDTO(1500, "MILLILITRE", "Volume");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(2, result, EPS); // 3000mL / 1500mL = 2
        }

        [TestMethod]
        public void testDivision_RatioGreaterThanOne()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result, EPS);
        }

        [TestMethod]
        public void testDivision_RatioLessThanOne()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(10, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(0.5, result, EPS);
        }

        [TestMethod]
        public void testDivision_RatioEqualToOne()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(10, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1, result, EPS);
        }

        [TestMethod]
        public void testDivision_NonCommutative()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act
            double aDivB = _arithmeticService.Divide(a, b);
            double bDivA = _arithmeticService.Divide(b, a);

            // Assert
            Assert.AreEqual(2, aDivB, EPS);
            Assert.AreEqual(0.5, bDivA, EPS);
        }

        [TestMethod]
        public void testDivision_WithLargeRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(1000000, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(1, "KILOGRAM", "Weight");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1000000, result, EPS);
        }

        [TestMethod]
        public void testDivision_WithSmallRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "KILOGRAM", "Weight");
            var q2 = new QuantityDTO(1000000, "KILOGRAM", "Weight");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(0.000001, result, EPS);
        }

        [TestMethod]
        public void testDivision_WithZeroNumerator()
        {
            // Arrange
            var q1 = new QuantityDTO(0, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(0, result, EPS);
        }

        //  INVERSE OPERATIONS TESTS 

        [TestMethod]
        public void testSubtractionAddition_Inverse()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act: (10 + 5) - 5 = 10
            var added = _arithmeticService.Add(a, b);
            var result = _arithmeticService.Subtract(added, b);

            // Assert
            Assert.AreEqual(a.Value, result.Value, EPS);
        }

        [TestMethod]
        public void testAdditionSubtraction_Inverse()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act: (10 - 5) + 5 = 10
            var subtracted = _arithmeticService.Subtract(a, b);
            var result = _arithmeticService.Add(subtracted, b);

            // Assert
            Assert.AreEqual(a.Value, result.Value, EPS);
        }

        //  IMMUTABILITY TESTS 

        [TestMethod]
        public void testSubtraction_Immutability()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(a, b);

            // Assert 
            Assert.AreEqual(10, a.Value);
            Assert.AreEqual(5, b.Value);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void testDivision_Immutability()
        {
            // Arrange
            var a = new QuantityDTO(10, "FEET", "Length");
            var b = new QuantityDTO(5, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(a, b);

            // Assert
            Assert.AreEqual(10, a.Value);
            Assert.AreEqual(5, b.Value);
            Assert.AreEqual(2, result, EPS);
        }

    }
}