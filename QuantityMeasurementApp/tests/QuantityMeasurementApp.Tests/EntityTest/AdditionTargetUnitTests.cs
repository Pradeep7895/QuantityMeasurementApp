using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests.EntityTest
{
    [TestClass]
    public class AdditionTargetUnitTests
    {
        private const double EPSILON = 1e-3;

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.FEET);

            Assert.AreEqual(2.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.INCH);

            Assert.AreEqual(24.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.YARD);

            Assert.AreEqual(0.667, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Centimeters()
        {
            var l1 = new Length(1.0, LengthUnit.INCH);
            var l2 = new Length(1.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.CENTIMETERS);

            Assert.AreEqual(5.08, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SameAsFirstOperand()
        {
            var l1 = new Length(2.0, LengthUnit.YARD);
            var l2 = new Length(3.0, LengthUnit.FEET);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.YARD);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SameAsSecondOperand()
        {
            var l1 = new Length(2.0, LengthUnit.YARD);
            var l2 = new Length(3.0, LengthUnit.FEET);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.FEET);

            Assert.AreEqual(9.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            var r1 = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.YARD);
            var r2 = QuantityMeasurement.demonstrateLengthAddition(l2, l1, LengthUnit.YARD);

            Assert.AreEqual(r1.Value, r2.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_WithZero()
        {
            var l1 = new Length(5.0, LengthUnit.FEET);
            var l2 = new Length(0.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.YARD);

            Assert.AreEqual(1.667, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_NegativeValues()
        {
            var l1 = new Length(5.0, LengthUnit.FEET);
            var l2 = new Length(-2.0, LengthUnit.FEET);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.INCH);

            Assert.AreEqual(36.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_LargeToSmallScale()
        {
            var l1 = new Length(1000.0, LengthUnit.FEET);
            var l2 = new Length(500.0, LengthUnit.FEET);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.INCH);

            Assert.AreEqual(18000.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SmallToLargeScale()
        {
            var l1 = new Length(12.0, LengthUnit.INCH);
            var l2 = new Length(12.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.YARD);

            Assert.AreEqual(0.667, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_AllUnitCombinations()
        {
            var f = new Length(1, LengthUnit.FEET);
            var i = new Length(12, LengthUnit.INCH);
            var y = new Length(1, LengthUnit.YARD);

            var r1 = QuantityMeasurement.demonstrateLengthAddition(f, i, LengthUnit.FEET);
            var r2 = QuantityMeasurement.demonstrateLengthAddition(i, y, LengthUnit.INCH);
            var r3 = QuantityMeasurement.demonstrateLengthAddition(y, f, LengthUnit.YARD);

            Assert.IsNotNull(r1);
            Assert.IsNotNull(r2);
            Assert.IsNotNull(r3);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_PrecisionTolerance()
        {
            var l1 = new Length(2.54, LengthUnit.CENTIMETERS);
            var l2 = new Length(1.0, LengthUnit.INCH);

            var result = QuantityMeasurement.demonstrateLengthAddition(l1, l2, LengthUnit.CENTIMETERS);

            Assert.AreEqual(5.08, result.Value, EPSILON);
        }
    }
}