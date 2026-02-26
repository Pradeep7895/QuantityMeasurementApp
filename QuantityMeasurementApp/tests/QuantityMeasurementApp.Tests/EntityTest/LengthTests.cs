using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class LengthTests
    {
        [TestMethod]
        public void testFeetEquality()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.FEET);
            Length l2 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod]
        public void testInchesEquality()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.INCHES);
            Length l2 = new Length(1.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod]
        public void testFeetInchesComparison()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.FEET);
            Length l2 = new Length(12.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod]
        public void testFeetInequality()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.FEET);
            Length l2 = new Length(2.0, Length.LengthUnit.FEET);

            Assert.IsFalse(l1.Equals(l2));
        }

        [TestMethod]
        public void testNullComparison()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsFalse(l1.Equals(null));
        }

        [TestMethod]
        public void testSameReference()
        {
            Length l1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(l1.Equals(l1));
        }
    }
}