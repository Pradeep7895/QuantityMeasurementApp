using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;
using System;
using System.Reflection;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityRefactoringTests
    {
        const double EPS = 0.01;

        LengthUnitHelper FEET = new LengthUnitHelper(LengthUnit.FEET);
        LengthUnitHelper INCH = new LengthUnitHelper(LengthUnit.INCH);

        WeightUnitHelper KG = new WeightUnitHelper(WeightUnit.KILOGRAM);
        WeightUnitHelper GRAM = new WeightUnitHelper(WeightUnit.GRAM);

        VolumeUnitHelper LITRE = new VolumeUnitHelper(VolumeUnit.LITRE);
        VolumeUnitHelper ML = new VolumeUnitHelper(VolumeUnit.MILLILITRE);

        // Helper Delegation Tests

        [TestMethod]
        public void testRefactoring_Add_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(5, FEET);

            var result = q1.Add(q2);

            Assert.AreEqual(15, result.Value);
        }

        [TestMethod]
        public void testRefactoring_Subtract_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(5, FEET);

            var result = q1.Subtract(q2);

            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void testRefactoring_Divide_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(2, FEET);

            var result = q1.Divide(q2);

            Assert.AreEqual(5, result, EPS);
        }

        // Arithmetic Behavior

        [TestMethod]
        public void testPerformBaseArithmetic_ConversionAndOperation()
        {
            var q1 = new Quantity<LengthUnitHelper>(1, FEET);
            var q2 = new Quantity<LengthUnitHelper>(12, INCH);

            var result = q1.Add(q2);

            Assert.AreEqual(2, result.Value, EPS);
        }

        // UC12 Backward Compatibility

        [TestMethod]
        public void testAdd_UC12_BehaviorPreserved()
        {
            var result = new Quantity<LengthUnitHelper>(1, FEET)
                .Add(new Quantity<LengthUnitHelper>(12, INCH));

            Assert.AreEqual(2, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtract_UC12_BehaviorPreserved()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(6, INCH));

            Assert.AreEqual(9.5, result.Value, EPS);
        }

        [TestMethod]
        public void testDivide_UC12_BehaviorPreserved()
        {
            double result = new Quantity<LengthUnitHelper>(24, INCH)
                .Divide(new Quantity<LengthUnitHelper>(2, FEET));

            Assert.AreEqual(1, result, EPS);
        }

        // Rounding Tests

        [TestMethod]
        public void testRounding_AddSubtract_TwoDecimalPlaces()
        {
            var q1 = new Quantity<LengthUnitHelper>(1.234, FEET);
            var q2 = new Quantity<LengthUnitHelper>(1.111, FEET);

            var result = q1.Add(q2);

            Assert.AreEqual(Math.Round(result.Value, 2), result.Value);
        }

        [TestMethod]
        public void testRounding_Divide_NoRounding()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(3, FEET);

            double result = q1.Divide(q2);

            Assert.AreEqual(3.3333333333333335, result);
        }


        // Target Unit Behavior

        [TestMethod]
        public void testImplicitTargetUnit_AddSubtract()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(12, INCH);

            var result = q1.Add(q2);

            Assert.AreEqual(FEET.GetType(), result.Unit.GetType());
        }

        [TestMethod]
        public void testExplicitTargetUnit_AddSubtract_Overrides()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(6, INCH);

            var result = q1.Subtract(q2, INCH);

            Assert.AreEqual(INCH.GetType(), result.Unit.GetType());
        }

        // Immutability Tests

        [TestMethod]
        public void testImmutability_AfterAdd_ViaCentralizedHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(5, FEET);

            q1.Add(q2);

            Assert.AreEqual(10, q1.Value);
        }

        [TestMethod]
        public void testImmutability_AfterSubtract_ViaCentralizedHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(5, FEET);

            q1.Subtract(q2);

            Assert.AreEqual(10, q1.Value);
        }

        [TestMethod]
        public void testImmutability_AfterDivide_ViaCentralizedHelper()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(2, FEET);

            q1.Divide(q2);

            Assert.AreEqual(10, q1.Value);
        }

        // All Categories

        [TestMethod]
        public void testAllOperations_AcrossAllCategories()
        {
            var l = new Quantity<LengthUnitHelper>(10, FEET)
                .Add(new Quantity<LengthUnitHelper>(5, FEET));

            var w = new Quantity<WeightUnitHelper>(10, KG)
                .Subtract(new Quantity<WeightUnitHelper>(5, KG));

            var v = new Quantity<VolumeUnitHelper>(10, LITRE)
                .Divide(new Quantity<VolumeUnitHelper>(2, LITRE));

            Assert.AreEqual(15, l.Value);
            Assert.AreEqual(5, w.Value);
            Assert.AreEqual(5, v);
        }

        // Arithmetic Chain Test

        [TestMethod]
        public void testArithmetic_Chain_Operations()
        {
            var q1 = new Quantity<LengthUnitHelper>(10, FEET);
            var q2 = new Quantity<LengthUnitHelper>(5, FEET);
            var q3 = new Quantity<LengthUnitHelper>(2, FEET);
            var q4 = new Quantity<LengthUnitHelper>(1, FEET);

            double result = q1.Add(q2).Subtract(q3).Divide(q4);

            Assert.AreEqual(13, result);
        }
    }
}