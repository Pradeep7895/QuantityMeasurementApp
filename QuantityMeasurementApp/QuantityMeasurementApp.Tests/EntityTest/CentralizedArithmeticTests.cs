using QuantityMeasurementApp.Service.Services;
using QuantityMeasurementApp.Service.Helpers;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QuantityMeasurementApp.Tests.EntityTest
{
    [TestClass]
    public class CentralizedArithmeticTests
    {
        // Constants
        private const double EPS = 0.01;

        // Unit helpers (kept for reference but not used directly in operations)
        private LengthUnitHelper FEET => new LengthUnitHelper(LengthUnit.FEET);
        private LengthUnitHelper INCH => new LengthUnitHelper(LengthUnit.INCH);
        private WeightUnitHelper KG => new WeightUnitHelper(WeightUnit.KILOGRAM);
        private WeightUnitHelper GRAM => new WeightUnitHelper(WeightUnit.GRAM);
        private VolumeUnitHelper LITRE => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper ML => new VolumeUnitHelper(VolumeUnit.MILLILITRE);

        // Services
        private ConversionService _conversionService;
        private ArithmeticService _arithmeticService;
        private EqualityService _equalityService;

        [TestInitialize]
        public void Setup()
        {
            // Initialize services for each test
            _conversionService = new ConversionService();
            _arithmeticService = new ArithmeticService(_conversionService);
            _equalityService = new EqualityService();
        }

        // HELPER DELEGATION TESTS 

        [TestMethod]
        public void testRefactoring_Add_DelegatesViaHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(15, result.Value);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testRefactoring_Subtract_DelegatesViaHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testRefactoring_Divide_DelegatesViaHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result, EPS);
        }

        // ARITHMETIC BEHAVIOR 

        [TestMethod]
        public void testPerformBaseArithmetic_ConversionAndOperation()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value, EPS);
            Assert.AreEqual("FEET", result.Unit);
        }

        // UC12 BACKWARD COMPATIBILITY

        [TestMethod]
        public void testAdd_UC12_BehaviorPreserved()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value, EPS);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testSubtract_UC12_BehaviorPreserved()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(9.5, result.Value, EPS);
            Assert.AreEqual("FEET", result.Unit);
        }

        [TestMethod]
        public void testDivide_UC12_BehaviorPreserved()
        {
            // Arrange
            var q1 = new QuantityDTO(24, "INCH", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1, result, EPS);
        }

        // ROUNDING TESTS 

        [TestMethod]
        public void testRounding_AddSubtract_TwoDecimalPlaces()
        {
            // Arrange
            var q1 = new QuantityDTO(1.234, "FEET", "Length");
            var q2 = new QuantityDTO(1.111, "FEET", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(Math.Round(result.Value, 2), result.Value);
            Assert.AreEqual(2.35, result.Value); // 1.234 + 1.111 = 2.345 → rounded to 2.35
        }

        [TestMethod]
        public void testRounding_Divide_NoRounding()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(3, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert 
            Assert.AreEqual(3.3333333333333335, result);
        }

        //TARGET UNIT BEHAVIOR

        [TestMethod]
        public void testImplicitTargetUnit_AddSubtract()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act - Implicit target unit 
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual("FEET", result.Unit);
            Assert.AreEqual(11, result.Value, EPS); // 10 feet + 1 foot = 11 feet
        }

        [TestMethod]
        public void testExplicitTargetUnit_AddSubtract_Overrides()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(6, "INCH", "Length");

            // Act 
            var result = _arithmeticService.Subtract(q1, q2, "INCH");

            // Assert
            Assert.AreEqual("INCH", result.Unit);
            Assert.AreEqual(114, result.Value, EPS); // 120 inches - 6 inches = 114 inches
        }

        [TestMethod]
        public void testAdd_WithExplicitTargetUnit()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act - Add with target unit YARD
            var result = _arithmeticService.Add(q1, q2, "YARD");

            // Assert
            Assert.AreEqual("YARD", result.Unit);
            Assert.AreEqual(0.67, result.Value, EPS); // 2 feet = 0.67 yards
        }

        [TestMethod]
        public void testSubtract_WithExplicitTargetUnit()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act - Subtract with target unit INCH
            var result = _arithmeticService.Subtract(q1, q2, "INCH");

            // Assert
            Assert.AreEqual("INCH", result.Unit);
            Assert.AreEqual(108, result.Value, EPS); // 120 inches - 12 inches = 108 inches
        }

        //IMMUTABILITY TESTS

        [TestMethod]
        public void testImmutability_AfterAdd_ViaCentralizedHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert 
            Assert.AreEqual(10, q1.Value);
            Assert.AreEqual(5, q2.Value);
            Assert.AreEqual(15, result.Value);
        }

        [TestMethod]
        public void testImmutability_AfterSubtract_ViaCentralizedHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(10, q1.Value);
            Assert.AreEqual(5, q2.Value);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void testImmutability_AfterDivide_ViaCentralizedHelper()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert 
            Assert.AreEqual(10, q1.Value);
            Assert.AreEqual(2, q2.Value);
            Assert.AreEqual(5, result);
        }

        //ALL CATEGORIES TESTS 

        [TestMethod]
        public void testAllOperations_AcrossAllCategories()
        {
            // Length addition
            var l1 = new QuantityDTO(10, "FEET", "Length");
            var l2 = new QuantityDTO(5, "FEET", "Length");
            var lResult = _arithmeticService.Add(l1, l2);

            // Weight subtraction
            var w1 = new QuantityDTO(10, "KILOGRAM", "Weight");
            var w2 = new QuantityDTO(5, "KILOGRAM", "Weight");
            var wResult = _arithmeticService.Subtract(w1, w2);

            // Volume division
            var v1 = new QuantityDTO(10, "LITRE", "Volume");
            var v2 = new QuantityDTO(2, "LITRE", "Volume");
            double vResult = _arithmeticService.Divide(v1, v2);

            // Assert
            Assert.AreEqual(15, lResult.Value);
            Assert.AreEqual(5, wResult.Value);
            Assert.AreEqual(5, vResult);
        }

        [TestMethod]
        public void testAllOperations_DifferentUnits_AcrossAllCategories()
        {
            // Length: Feet + Inches
            var l1 = new QuantityDTO(1, "FEET", "Length");
            var l2 = new QuantityDTO(12, "INCH", "Length");
            var lResult = _arithmeticService.Add(l1, l2);

            // Weight: Kg + Grams
            var w1 = new QuantityDTO(1, "KILOGRAM", "Weight");
            var w2 = new QuantityDTO(500, "GRAM", "Weight");
            var wResult = _arithmeticService.Add(w1, w2);

            // Volume: Litre + Millilitre
            var v1 = new QuantityDTO(1, "LITRE", "Volume");
            var v2 = new QuantityDTO(500, "MILLILITRE", "Volume");
            var vResult = _arithmeticService.Add(v1, v2);

            // Assert
            Assert.AreEqual(2, lResult.Value, EPS);
            Assert.AreEqual(1.5, wResult.Value, EPS);
            Assert.AreEqual(1.5, vResult.Value, EPS);
        }

        // ARITHMETIC CHAIN TESTS 

        [TestMethod]
        public void testArithmetic_Chain_Operations()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");
            var q3 = new QuantityDTO(2, "FEET", "Length");
            var q4 = new QuantityDTO(1, "FEET", "Length");

            // Act - Chain operations: (10 + 5 - 2) / 1 = 13
            var addResult = _arithmeticService.Add(q1, q2);
            var subResult = _arithmeticService.Subtract(addResult, q3);
            double finalResult = _arithmeticService.Divide(subResult, q4);

            // Assert
            Assert.AreEqual(13, finalResult);
        }

        [TestMethod]
        public void testComplex_Arithmetic_Chain_WithDifferentUnits()
        {
            // Arrange
            var q1 = new QuantityDTO(3, "FEET", "Length");      // 3 feet
            var q2 = new QuantityDTO(24, "INCH", "Length");     // 2 feet
            var q3 = new QuantityDTO(1, "YARD", "Length");      // 3 feet
            var q4 = new QuantityDTO(12, "INCH", "Length");     // 1 foot

            // Act: (3 feet + 2 feet - 3 feet) / 1 foot = 2
            var addResult = _arithmeticService.Add(q1, q2);           // 3ft + 2ft = 5ft
            var subResult = _arithmeticService.Subtract(addResult, q3); // 5ft - 3ft = 2ft
            double finalResult = _arithmeticService.Divide(subResult, q4); // 2ft / 1ft = 2

            // Assert
            Assert.AreEqual(2, finalResult, EPS);
        }

        // NEGATIVE VALUE TESTS

        [TestMethod]
        public void testAdd_WithNegativeValues()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(-3, "FEET", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(7, result.Value);
        }

        [TestMethod]
        public void testSubtract_WithNegativeValues()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(-3, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(13, result.Value); // 10 - (-3) = 13
        }

        [TestMethod]
        public void testDivide_WithNegativeValues()
        {
            // Arrange
            var q1 = new QuantityDTO(-10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            double result = _arithmeticService.Divide(q1, q2);

            // Assert
            Assert.AreEqual(-5, result, EPS);
        }

        // ZERO VALUE TESTS 

        [TestMethod]
        public void testAdd_WithZero()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(0, "FEET", "Length");

            // Act
            var result = _arithmeticService.Add(q1, q2);

            // Assert
            Assert.AreEqual(10, result.Value);
        }

        [TestMethod]
        public void testSubtract_WithZero()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(0, "FEET", "Length");

            // Act
            var result = _arithmeticService.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(10, result.Value);
        }

    }
}