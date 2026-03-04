using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class AdditionTests
    {
        private const double EPSILON = 1e-6;

        //Same Unit – Feet
        [TestMethod]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            var l1 = new Length(1.0, Length.LengthUnit.FEET);
            var l2 = new Length(2.0, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(3.0, result.Value, EPSILON);
            Assert.AreEqual(Length.LengthUnit.FEET, result.Unit);
        }

        //Same Unit – Inches
        [TestMethod]
        public void testAddition_SameUnit_InchPlusInch()
        {
            var l1 = new Length(6.0, Length.LengthUnit.INCH);
            var l2 = new Length(6.0, Length.LengthUnit.INCH);

            var result = l1.add(l2);

            Assert.AreEqual(12.0, result.Value, EPSILON);
            Assert.AreEqual(Length.LengthUnit.INCH, result.Unit);
        }

        //Cross Unit – Feet + Inches
        [TestMethod]
        public void testAddition_CrossUnit_FeetPlusInches()
        {
            var l1 = new Length(1.0, Length.LengthUnit.FEET);
            var l2 = new Length(12.0, Length.LengthUnit.INCH);

            var result = l1.add(l2);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(Length.LengthUnit.FEET, result.Unit);
        }

        // Cross Unit – Inch + Feet
        [TestMethod]
        public void testAddition_CrossUnit_InchPlusFeet()
        {
            var l1 = new Length(12.0, Length.LengthUnit.INCH);
            var l2 = new Length(1.0, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(24.0, result.Value, EPSILON);
            Assert.AreEqual(Length.LengthUnit.INCH, result.Unit);
        }

        // Yard + Feet
        [TestMethod]
        public void testAddition_CrossUnit_YardPlusFeet()
        {
            var l1 = new Length(1.0, Length.LengthUnit.YARD);
            var l2 = new Length(3.0, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(Length.LengthUnit.YARD, result.Unit);
        }

        //Centimeter + Inch
        [TestMethod]
        public void testAddition_CrossUnit_CentimeterPlusInch()
        {
            var l1 = new Length(2.54, Length.LengthUnit.CENTIMETERS);
            var l2 = new Length(1.0, Length.LengthUnit.INCH);

            var result = l1.add(l2);

            Assert.AreEqual(5.08, result.Value, 0.01); // small tolerance
            Assert.AreEqual(Length.LengthUnit.CENTIMETERS, result.Unit);
        }

        // Commutativity
        [TestMethod]
        public void testAddition_Commutativity()
        {
            var l1 = new Length(1.0, Length.LengthUnit.FEET);
            var l2 = new Length(12.0, Length.LengthUnit.INCH);

            var result1 = l1.add(l2);
            var result2 = l2.add(l1);

            // convert both to base and compare
            Assert.IsTrue(result1.Equals(result2));
        }

        //Identity (Zero)
        [TestMethod]
        public void testAddition_WithZero()
        {
            var l1 = new Length(5.0, Length.LengthUnit.FEET);
            var l2 = new Length(0.0, Length.LengthUnit.INCH);

            var result = l1.add(l2);

            Assert.AreEqual(5.0, result.Value, EPSILON);
        }

        // Negative Values
        [TestMethod]
        public void testAddition_NegativeValues()
        {
            var l1 = new Length(5.0, Length.LengthUnit.FEET);
            var l2 = new Length(-2.0, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }

        //Large Values
        [TestMethod]
        public void testAddition_LargeValues()
        {
            var l1 = new Length(1e6, Length.LengthUnit.FEET);
            var l2 = new Length(1e6, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(2e6, result.Value, EPSILON);
        }

        //Small Values
        [TestMethod]
        public void testAddition_SmallValues()
        {
            var l1 = new Length(0.001, Length.LengthUnit.FEET);
            var l2 = new Length(0.002, Length.LengthUnit.FEET);

            var result = l1.add(l2);

            Assert.AreEqual(0.003, result.Value, EPSILON);
        }
    }
}