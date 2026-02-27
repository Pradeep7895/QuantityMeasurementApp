using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;
using static QuantityMeasurementApp.Domain.Length;

// UC -4

namespace QuantityMeasurementApp.Test.EntityTest
{
    [TestClass]
    public class LengthTest
    {
        // SAME UNIT EQUALITY TESTS

        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            var a = new Length(1.0, LengthUnit.FEET);
            var b = new Length(1.0, LengthUnit.FEET);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void TestEquality_InchesToInches_SameValue()
        {
            var a = new Length(1.0, LengthUnit.INCH);
            var b = new Length(1.0, LengthUnit.INCH);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void TestEquality_YardToYard_SameValue()
        {
            var a = new Length(1.0, LengthUnit.YARD);
            var b = new Length(1.0, LengthUnit.YARD);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void TestEquality_CentimeterToCentimeter_SameValue()
        {
            var a = new Length(2.0, LengthUnit.CENTIMETERS);
            var b = new Length(2.0, LengthUnit.CENTIMETERS);

            Assert.IsTrue(a.Equals(b));
        }

        // CROSS UNIT EQUALITY TESTS

        [TestMethod]
        public void TestEquality_FeetToInches_EquivalentValue()
        {
            var feet = new Length(1.0, LengthUnit.FEET);
            var inches = new Length(12.0, LengthUnit.INCH);

            Assert.IsTrue(feet.Equals(inches));
        }

        [TestMethod]
        public void TestEquality_YardToFeet_EquivalentValue()
        {
            var yard = new Length(1.0, LengthUnit.YARD);
            var feet = new Length(3.0, LengthUnit.FEET);

            Assert.IsTrue(yard.Equals(feet));
        }

        [TestMethod]
        public void TestEquality_YardToInches_EquivalentValue()
        {
            var yard = new Length(1.0, LengthUnit.YARD);
            var inches = new Length(36.0, LengthUnit.INCH);

            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void TestEquality_CentimetersToInches_EquivalentValue()
        {
            var cm = new Length(1.0, LengthUnit.CENTIMETERS);
            var inches = new Length(0.393701, LengthUnit.INCH);

            Assert.IsTrue(cm.Equals(inches));
        }

        // NON-EQUIVALENT TESTS

        [TestMethod]
        public void TestEquality_YardToFeet_NonEquivalentValue()
        {
            var yard = new Length(1.0, LengthUnit.YARD);
            var feet = new Length(2.0, LengthUnit.FEET);

            Assert.IsFalse(yard.Equals(feet));
        }

        [TestMethod]
        public void TestEquality_CentimetersToFeet_NonEquivalentValue()
        {
            var cm = new Length(1.0, LengthUnit.CENTIMETERS);
            var feet = new Length(1.0, LengthUnit.FEET);

            Assert.IsFalse(cm.Equals(feet));
        }

        // TRANSITIVE PROPERTY TEST

        [TestMethod]
        public void TestEquality_MultiUnit_TransitiveProperty()
        {
            var yard = new Length(1.0, LengthUnit.YARD);     
            var feet = new Length(3.0, LengthUnit.FEET);      
            var inches = new Length(36.0, LengthUnit.INCH);   

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }

        // EQUALITY CONTRACT TESTS

        [TestMethod]
        public void TestEquality_SameReference()
        {
            var a = new Length(1.0, LengthUnit.YARD);
            Assert.IsTrue(a.Equals(a));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var a = new Length(1.0, LengthUnit.CENTIMETERS);
            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void TestEquality_DifferentType()
        {
            var a = new Length(1.0, LengthUnit.YARD);
            object other = "Not a length";

            Assert.IsFalse(a.Equals(other));
        }

        // ENUM VALIDATION TESTS 

        [TestMethod]
        public void TestUnitParsing_InvalidUnit_ShouldFail()
        {
            bool result = Enum.TryParse("METER", true, out LengthUnit _);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestUnitParsing_ValidUnits_ShouldPass()
        {
            bool okYard = Enum.TryParse("YARD", true, out LengthUnit u1);
            bool okCm = Enum.TryParse("CENTIMETERS", true, out LengthUnit u2);

            Assert.IsTrue(okYard);
            Assert.AreEqual(LengthUnit.YARD, u1);

            Assert.IsTrue(okCm);
            Assert.AreEqual(LengthUnit.CENTIMETERS, u2);
        }
    }
}