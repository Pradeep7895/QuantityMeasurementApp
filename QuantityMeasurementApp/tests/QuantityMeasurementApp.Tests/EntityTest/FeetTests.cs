using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void TestFeetEquality_SameValue()
        {
            var f1 = new Feet(1.0);
            var f2 = new Feet(1.0);

            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod]
        public void TestFeetEquality_DifferentValue()
        {
            var f1 = new Feet(1.0);
            var f2 = new Feet(2.0);

            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod]
        public void TestFeetEquality_NullComparison()
        {
            var f1 = new Feet(1.0);

            Assert.IsFalse(f1.Equals(null));
        }

        [TestMethod]
        public void TestFeetEquality_SameReference()
        {
            var f1 = new Feet(1.0);

            Assert.IsTrue(f1.Equals(f1));
        }

        [TestMethod]
        public void TestFeetEquality_DifferentType()
        {
            var f1 = new Feet(1.0);

            Assert.IsFalse(f1.Equals(new object()));
        }
    }
}