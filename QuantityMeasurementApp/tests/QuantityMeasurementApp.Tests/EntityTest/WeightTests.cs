using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class WeightTests
    {
        private const double EPSILON = 1e-3;

        // Equality Tests

        [TestMethod]
        public void testEquality_KilogramToKilogram_SameValue()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_KilogramToKilogram_DifferentValue()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(2.0, WeightUnit.KILOGRAM);

            Assert.IsFalse(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_KilogramToGram_EquivalentValue()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000.0, WeightUnit.GRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_GramToKilogram_EquivalentValue()
        {
            var w1 = new Weight(1000.0, WeightUnit.GRAM);
            var w2 = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsFalse(w1.Equals(null));
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(w1.Equals(w1));
        }

        [TestMethod]
        public void testEquality_TransitiveProperty()
        {
            var a = new Weight(1.0, WeightUnit.KILOGRAM);
            var b = new Weight(1000.0, WeightUnit.GRAM);
            var c = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void testEquality_ZeroValue()
        {
            var w1 = new Weight(0.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(0.0, WeightUnit.GRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_NegativeWeight()
        {
            var w1 = new Weight(-1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(-1000.0, WeightUnit.GRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_LargeWeightValue()
        {
            var w1 = new Weight(1000000.0, WeightUnit.GRAM);
            var w2 = new Weight(1000.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testEquality_SmallWeightValue()
        {
            var w1 = new Weight(0.001, WeightUnit.KILOGRAM);
            var w2 = new Weight(1.0, WeightUnit.GRAM);

            Assert.IsTrue(w1.Equals(w2));
        }

        // Conversion Tests
        [TestMethod]
        public void testConversion_PoundToKilogram()
        {
            var w = new Weight(2.20462, WeightUnit.POUND);

            var result = w.convertTo(WeightUnit.KILOGRAM);

            Assert.AreEqual(1.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testConversion_KilogramToPound()
        {
            var w = new Weight(1.0, WeightUnit.KILOGRAM);

            var result = w.convertTo(WeightUnit.POUND);

            Assert.AreEqual(2.20462, result.Value, EPSILON);
        }

        [TestMethod]
        public void testConversion_SameUnit()
        {
            var w = new Weight(5.0, WeightUnit.KILOGRAM);

            var result = w.convertTo(WeightUnit.KILOGRAM);

            Assert.AreEqual(5.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            var w = new Weight(0.0, WeightUnit.KILOGRAM);

            var result = w.convertTo(WeightUnit.GRAM);

            Assert.AreEqual(0.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            var w = new Weight(-1.0, WeightUnit.KILOGRAM);

            var result = w.convertTo(WeightUnit.GRAM);

            Assert.AreEqual(-1000.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            var w = new Weight(1.5, WeightUnit.KILOGRAM);

            var result = w.convertTo(WeightUnit.GRAM)
                            .convertTo(WeightUnit.KILOGRAM);

            Assert.AreEqual(1.5, result.Value, EPSILON);
        }

        // Addition Tests
        [TestMethod]
        public void testAddition_SameUnit_KilogramPlusKilogram()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(2.0, WeightUnit.KILOGRAM);

            var result = w1.add(w2);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_CrossUnit_KilogramPlusGram()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000.0, WeightUnit.GRAM);

            var result = w1.add(w2);

            Assert.AreEqual(2.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_CrossUnit_PoundPlusKilogram()
        {
            var w1 = new Weight(2.20462, WeightUnit.POUND);
            var w2 = new Weight(1.0, WeightUnit.KILOGRAM);

            var result = w1.add(w2);

            Assert.AreEqual(4.40924, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Kilogram()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000.0, WeightUnit.GRAM);

            var result = w1.add(w2, WeightUnit.GRAM);

            Assert.AreEqual(2000.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_Commutativity()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000.0, WeightUnit.GRAM);

            var r1 = w1.add(w2);
            var r2 = w2.add(w1);

            Assert.AreEqual(r1.convertTo(WeightUnit.KILOGRAM).Value,
                            r2.convertTo(WeightUnit.KILOGRAM).Value,
                            EPSILON);
        }

        [TestMethod]
        public void testAddition_WithZero()
        {
            var w1 = new Weight(5.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(0.0, WeightUnit.GRAM);

            var result = w1.add(w2);

            Assert.AreEqual(5.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_NegativeValues()
        {
            var w1 = new Weight(5.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(-2000.0, WeightUnit.GRAM);

            var result = w1.add(w2);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_LargeValues()
        {
            var w1 = new Weight(1000000.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000000.0, WeightUnit.KILOGRAM);

            var result = w1.add(w2);

            Assert.AreEqual(2000000.0, result.Value, EPSILON);
        }
    }
}