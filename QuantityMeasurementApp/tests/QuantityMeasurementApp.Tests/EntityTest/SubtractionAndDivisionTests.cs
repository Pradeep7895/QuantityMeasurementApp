using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantitySubtractionDivisionTests
    {
        const double EPS = 0.01;

        private LengthUnitHelper FEET => new LengthUnitHelper(LengthUnit.FEET);
        private LengthUnitHelper INCH => new LengthUnitHelper(LengthUnit.INCH);

        private WeightUnitHelper KG => new WeightUnitHelper(WeightUnit.KILOGRAM);
        private WeightUnitHelper GRAM => new WeightUnitHelper(WeightUnit.GRAM);

        private VolumeUnitHelper LITRE => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper ML => new VolumeUnitHelper(VolumeUnit.MILLILITRE);

        // SUBTRACTION 

        [TestMethod]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(5, FEET));

            Assert.AreEqual(5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            var result = new Quantity<VolumeUnitHelper>(10, LITRE)
                .Subtract(new Quantity<VolumeUnitHelper>(3, LITRE));

            Assert.AreEqual(7, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(6, INCH));

            Assert.AreEqual(9.5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_InchesMinusFeet()
        {
            var result = new Quantity<LengthUnitHelper>(120, INCH)
                .Subtract(new Quantity<LengthUnitHelper>(5, FEET));

            Assert.AreEqual(60, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Feet()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(6, INCH), FEET);

            Assert.AreEqual(9.5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Inches()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(6, INCH), INCH);

            Assert.AreEqual(114, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            var result = new Quantity<VolumeUnitHelper>(5, LITRE)
                .Subtract(new Quantity<VolumeUnitHelper>(2, LITRE), ML);

            Assert.AreEqual(3000, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ResultingInNegative()
        {
            var result = new Quantity<LengthUnitHelper>(5, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(10, FEET));

            Assert.AreEqual(-5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ResultingInZero()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(120, INCH));

            Assert.AreEqual(0, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithZeroOperand()
        {
            var result = new Quantity<LengthUnitHelper>(5, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(0, INCH));

            Assert.AreEqual(5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithNegativeValues()
        {
            var result = new Quantity<LengthUnitHelper>(5, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(-2, FEET));

            Assert.AreEqual(7, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_NonCommutative()
        {
            var a = new Quantity<LengthUnitHelper>(10, FEET);
            var b = new Quantity<LengthUnitHelper>(5, FEET);

            Assert.AreEqual(5, a.Subtract(b).Value, EPS);
            Assert.AreEqual(-5, b.Subtract(a).Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithLargeValues()
        {
            var result = new Quantity<WeightUnitHelper>(1e6, KG)
                .Subtract(new Quantity<WeightUnitHelper>(5e5, KG));

            Assert.AreEqual(5e5, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_WithSmallValues()
        {
            var result = new Quantity<LengthUnitHelper>(0.001, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(0.0005, FEET));

            Assert.AreEqual(0.0005, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_ChainedOperations()
        {
            var result = new Quantity<LengthUnitHelper>(10, FEET)
                .Subtract(new Quantity<LengthUnitHelper>(2, FEET))
                .Subtract(new Quantity<LengthUnitHelper>(1, FEET));

            Assert.AreEqual(7, result.Value, EPS);
        }

        // DIVISION 

        [TestMethod]
        public void testDivision_SameUnit_FeetDividedByFeet()
        {
            double result = new Quantity<LengthUnitHelper>(10, FEET)
                .Divide(new Quantity<LengthUnitHelper>(2, FEET));

            Assert.AreEqual(5, result, EPS);
        }

        [TestMethod]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            double result = new Quantity<VolumeUnitHelper>(10, LITRE)
                .Divide(new Quantity<VolumeUnitHelper>(5, LITRE));

            Assert.AreEqual(2, result, EPS);
        }

        [TestMethod]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            double result = new Quantity<LengthUnitHelper>(24, INCH)
                .Divide(new Quantity<LengthUnitHelper>(2, FEET));

            Assert.AreEqual(1, result, EPS);
        }

        [TestMethod]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            double result = new Quantity<WeightUnitHelper>(2, KG)
                .Divide(new Quantity<WeightUnitHelper>(2000, GRAM));

            Assert.AreEqual(1, result, EPS);
        }

        [TestMethod]
        public void testDivision_RatioGreaterThanOne()
        {
            double result = new Quantity<LengthUnitHelper>(10, FEET)
                .Divide(new Quantity<LengthUnitHelper>(2, FEET));

            Assert.AreEqual(5, result, EPS);
        }

        [TestMethod]
        public void testDivision_RatioLessThanOne()
        {
            double result = new Quantity<LengthUnitHelper>(5, FEET)
                .Divide(new Quantity<LengthUnitHelper>(10, FEET));

            Assert.AreEqual(0.5, result, EPS);
        }

        [TestMethod]
        public void testDivision_RatioEqualToOne()
        {
            double result = new Quantity<LengthUnitHelper>(10, FEET)
                .Divide(new Quantity<LengthUnitHelper>(10, FEET));

            Assert.AreEqual(1, result, EPS);
        }

        [TestMethod]
        public void testDivision_NonCommutative()
        {
            var a = new Quantity<LengthUnitHelper>(10, FEET);
            var b = new Quantity<LengthUnitHelper>(5, FEET);

            Assert.AreEqual(2, a.Divide(b), EPS);
            Assert.AreEqual(0.5, b.Divide(a), EPS);
        }

        [TestMethod]
        public void testDivision_WithLargeRatio()
        {
            double result = new Quantity<WeightUnitHelper>(1e6, KG)
                .Divide(new Quantity<WeightUnitHelper>(1, KG));

            Assert.AreEqual(1e6, result, EPS);
        }

        [TestMethod]
        public void testDivision_WithSmallRatio()
        {
            double result = new Quantity<WeightUnitHelper>(1, KG)
                .Divide(new Quantity<WeightUnitHelper>(1e6, KG));

            Assert.AreEqual(1e-6, result, EPS);
        }

        [TestMethod]
        public void testSubtractionAddition_Inverse()
        {
            var a = new Quantity<LengthUnitHelper>(10, FEET);
            var b = new Quantity<LengthUnitHelper>(5, FEET);

            var result = a.Add(b).Subtract(b);

            Assert.AreEqual(a.Value, result.Value, EPS);
        }

        [TestMethod]
        public void testSubtraction_Immutability()
        {
            var a = new Quantity<LengthUnitHelper>(10, FEET);
            var b = new Quantity<LengthUnitHelper>(5, FEET);

            var result = a.Subtract(b);

            Assert.AreEqual(10, a.Value);
            Assert.AreEqual(5, b.Value);
        }

        [TestMethod]
        public void testDivision_Immutability()
        {
            var a = new Quantity<LengthUnitHelper>(10, FEET);
            var b = new Quantity<LengthUnitHelper>(5, FEET);

            a.Divide(b);

            Assert.AreEqual(10, a.Value);
            Assert.AreEqual(5, b.Value);
        }
    }
}