using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

// UC-5 Unit -to -Unit Conversion

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class ConversionTests
    {
        private const double EPSILON = 1e-6;

        [TestMethod]
        public void TestConversion_FeetToInches()
        {
            double result = Length.convert(1.0, Length.LengthUnit.FEET, Length.LengthUnit.INCH);

            Assert.AreEqual(12.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_InchesToFeet()
        {
            double result = Length.convert(24.0, Length.LengthUnit.INCH, Length.LengthUnit.FEET);

            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_YardsToInches()
        {
            double result = Length.convert(1.0, Length.LengthUnit.YARD, Length.LengthUnit.INCH);

            Assert.AreEqual(36.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_YardToFeet()
        {
            double result = Length.convert(3.0, Length.LengthUnit.YARD, Length.LengthUnit.FEET);
            Assert.AreEqual(9.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_InchToYard()
        {
            double result = Length.convert(72.0, Length.LengthUnit.INCH, Length.LengthUnit.YARD);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_CentimetersToInches()
        {
            double result = Length.convert(2.54, Length.LengthUnit.CENTIMETERS, Length.LengthUnit.INCH);

            Assert.AreEqual(1.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_ZeroValue()
        {
            double result = Length.convert(0.0, Length.LengthUnit.FEET, Length.LengthUnit.INCH);

            Assert.AreEqual(0.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_NegativeValue()
        {
            double result = Length.convert(-1.0, Length.LengthUnit.FEET, Length.LengthUnit.INCH);

            Assert.AreEqual(-12.0, result, EPSILON);
        }

        [TestMethod]
        public void TestConversion_RoundTrip()
        {
            double original = 5.0;

            double inches = Length.convert(original, Length.LengthUnit.FEET, Length.LengthUnit.INCH);

            double feet = Length.convert(inches, Length.LengthUnit.INCH, Length.LengthUnit.FEET);

            Assert.AreEqual(original, feet, EPSILON);
        }

        [TestMethod]
        public void testConversion_NaN_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                Length.convert(double.NaN, Length.LengthUnit.FEET, Length.LengthUnit.INCH)
            );
            
        }
    }
}