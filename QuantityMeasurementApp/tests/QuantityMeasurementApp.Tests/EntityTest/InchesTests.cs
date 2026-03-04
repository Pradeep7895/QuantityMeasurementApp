
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void Inches_SameValue_ShouldReturnTrue()
        {
            // Arrange: Create two Inches objects with same value
            var inch1 = new Inches(1.0);
            var inch2 = new Inches(1.0);

            // Act & Assert: Objects with same value should be equal
            Assert.IsTrue(inch1.Equals(inch2));
        }

        [TestMethod]
        public void Inches_DifferentValue_ShouldReturnFalse()
        {   
            // Arrange: Create two Inches objects with different values
            var inch1 = new Inches(1.0);
            var inch2 = new Inches(2.0);

            // Act & Assert: Objects with different values should not be equal
            Assert.IsFalse(inch1.Equals(inch2));
        }

        [TestMethod]
        public void Inches_NullComparison_ShouldReturnFalse()
        {
            // Arrange: Create an Inches object
            var inch = new Inches(1.0);

            // Act & Assert: Comparing with null should return false
            Assert.IsFalse(inch.Equals(null));
        }

        [TestMethod]
        public void Inches_SameReference_ShouldReturnTrue()
        {
            // Arrange: Create a single Inches object
            var inch = new Inches(1.0);

            // Act & Assert: Object should be equal to itself
            Assert.IsTrue(inch.Equals(inch));
        }

        [TestMethod]
        public void Inches_DifferentType_ShouldReturnFalse()
        {
            // Arrange: Create an Inches object and a Feet object
            var inch = new Inches(1.0);
            var feet = new Feet(1.0);
            
            // Act & Assert: Objects of different types should not be equal
            Assert.IsFalse(inch.Equals(feet));
        }
    }
}