using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class VolumeQuantityTests
    {
        private const double EPS = 1e-3;

        private VolumeUnitHelper L => new VolumeUnitHelper(VolumeUnit.LITRE);
        private VolumeUnitHelper ML => new VolumeUnitHelper(VolumeUnit.MILLILITRE);
        private VolumeUnitHelper GAL => new VolumeUnitHelper(VolumeUnit.GALLON);

        // ---------- Equality ----------

        [TestMethod]
        public void testEquality_LitreToLitre_SameValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_LitreToLitre_DifferentValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(2.0, L);
            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_LitreToMillilitre_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_MillilitreToLitre_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            var q2 = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_LitreToGallon_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(0.264172, GAL);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_GallonToLitre_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, GAL);
            var q2 = new Quantity<VolumeUnitHelper>(3.78541, L);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_VolumeVsLength_Incompatible()
        {
            var v = new Quantity<VolumeUnitHelper>(1.0, L);
            var l = new Quantity<LengthUnitHelper>(1.0, new LengthUnitHelper(LengthUnit.FEET));
            Assert.IsFalse(v.Equals(l));
        }

        [TestMethod]
        public void testEquality_VolumeVsWeight_Incompatible()
        {
            var v = new Quantity<VolumeUnitHelper>(1.0, L);
            var w = new Quantity<WeightUnitHelper>(1.0, new WeightUnitHelper(WeightUnit.KILOGRAM));
            Assert.IsFalse(v.Equals(w));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var q = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.IsFalse(q.Equals(null));
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var q = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.IsTrue(q.Equals(q));
        }

        [TestMethod]
        public void testEquality_TransitiveProperty()
        {
            var a = new Quantity<VolumeUnitHelper>(1.0, L);
            var b = new Quantity<VolumeUnitHelper>(1000.0, ML);
            var c = new Quantity<VolumeUnitHelper>(1.0, L);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void testEquality_ZeroValue()
        {
            var a = new Quantity<VolumeUnitHelper>(0.0, L);
            var b = new Quantity<VolumeUnitHelper>(0.0, ML);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_NegativeVolume()
        {
            var a = new Quantity<VolumeUnitHelper>(-1.0, L);
            var b = new Quantity<VolumeUnitHelper>(-1000.0, ML);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_LargeVolumeValue()
        {
            var a = new Quantity<VolumeUnitHelper>(1000000.0, ML);
            var b = new Quantity<VolumeUnitHelper>(1000.0, L);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_SmallVolumeValue()
        {
            var a = new Quantity<VolumeUnitHelper>(0.001, L);
            var b = new Quantity<VolumeUnitHelper>(1.0, ML);
            Assert.IsTrue(a.Equals(b));
        }

        // ---------- Conversion ----------

        [TestMethod]
        public void testConversion_LitreToMillilitre()
        {
            var q = new Quantity<VolumeUnitHelper>(1.0, L);
            var r = q.ConvertTo(ML);
            Assert.AreEqual(1000.0, r, EPS);
        }

        [TestMethod]
        public void testConversion_MillilitreToLitre()
        {
            var q = new Quantity<VolumeUnitHelper>(1000.0, ML);
            var r = q.ConvertTo(L);
            Assert.AreEqual(1.0, r, EPS);
        }

        [TestMethod]
        public void testConversion_GallonToLitre()
        {
            var q = new Quantity<VolumeUnitHelper>(1.0, GAL);
            var r = q.ConvertTo(L);
            Assert.AreEqual(3.78541, r, EPS);
        }

        [TestMethod]
        public void testConversion_LitreToGallon()
        {
            var q = new Quantity<VolumeUnitHelper>(3.78541, L);
            var r = q.ConvertTo(GAL);
            Assert.AreEqual(1.0, r, EPS);
        }

        [TestMethod]
        public void testConversion_MillilitreToGallon()
        {
            var q = new Quantity<VolumeUnitHelper>(1000.0, ML);
            var r = q.ConvertTo(GAL);
            Assert.AreEqual(0.264172, r, EPS);
        }

        [TestMethod]
        public void testConversion_SameUnit()
        {
            var q = new Quantity<VolumeUnitHelper>(5.0, L);
            Assert.AreEqual(5.0, q.ConvertTo(L), EPS);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            var q = new Quantity<VolumeUnitHelper>(0.0, L);
            Assert.AreEqual(0.0, q.ConvertTo(ML), EPS);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            var q = new Quantity<VolumeUnitHelper>(-1.0, L);
            Assert.AreEqual(-1000.0, q.ConvertTo(ML), EPS);
        }

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            var q = new Quantity<VolumeUnitHelper>(1.5, L);
            var ml = q.ConvertTo(ML);
            var back = new Quantity<VolumeUnitHelper>(ml, ML).ConvertTo(L);
            Assert.AreEqual(1.5, back, EPS);
        }

        // ---------- Addition ----------

        [TestMethod]
        public void testAddition_SameUnit_LitrePlusLitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(2.0, L);
            Assert.AreEqual(3.0, q1.Add(q2).Value, EPS);
        }

        [TestMethod]
        public void testAddition_SameUnit_MillilitrePlusMillilitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(500.0, ML);
            var q2 = new Quantity<VolumeUnitHelper>(500.0, ML);
            Assert.AreEqual(1000.0, q1.Add(q2).Value, EPS);
        }

        [TestMethod]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            Assert.AreEqual(2.0, q1.Add(q2).Value, EPS);
        }

        [TestMethod]
        public void testAddition_CrossUnit_MillilitrePlusLitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            var q2 = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.AreEqual(2000.0, q1.Add(q2).Value, EPS);
        }

        [TestMethod]
        public void testAddition_CrossUnit_GallonPlusLitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, GAL);
            var q2 = new Quantity<VolumeUnitHelper>(3.78541, L);
            Assert.AreEqual(2.0, q1.Add(q2).Value, EPS);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Litre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            Assert.AreEqual(2.0, q1.Add(q2, L).Value, EPS);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            var q1 = new Quantity<VolumeUnitHelper>(1.0, L);
            var q2 = new Quantity<VolumeUnitHelper>(1000.0, ML);
            Assert.AreEqual(2000.0, q1.Add(q2, ML).Value, EPS);
        }

        [TestMethod]
        public void testAddition_WithZero()
        {
            var a = new Quantity<VolumeUnitHelper>(5.0, L);
            var b = new Quantity<VolumeUnitHelper>(0.0, ML);
            Assert.AreEqual(5.0, a.Add(b).Value, EPS);
        }

        [TestMethod]
        public void testAddition_NegativeValues()
        {
            var a = new Quantity<VolumeUnitHelper>(5.0, L);
            var b = new Quantity<VolumeUnitHelper>(-2000.0, ML);
            Assert.AreEqual(3.0, a.Add(b).Value, EPS);
        }

        // ---------- Enum / Helper ----------

        [TestMethod]
        public void testVolumeUnitEnum_LitreConstant()
        {
            Assert.AreEqual(1.0, L.GetConversionFactor(), EPS);
        }

        [TestMethod]
        public void testVolumeUnitEnum_MillilitreConstant()
        {
            Assert.AreEqual(0.001, ML.GetConversionFactor(), EPS);
        }

        [TestMethod]
        public void testVolumeUnitEnum_GallonConstant()
        {
            Assert.AreEqual(3.78541, GAL.GetConversionFactor(), EPS);
        }

        // ---------- Architecture ----------

        [TestMethod]
        public void testGenericQuantity_VolumeOperations_Consistency()
        {
            var q = new Quantity<VolumeUnitHelper>(1.0, L);
            Assert.IsNotNull(q);
        }

        [TestMethod]
        public void testScalability_VolumeIntegration()
        {
            var q = new Quantity<VolumeUnitHelper>(2.0, L);
            var converted = q.ConvertTo(ML);
            Assert.AreEqual(2000.0, converted, EPS);
        }
    }
}