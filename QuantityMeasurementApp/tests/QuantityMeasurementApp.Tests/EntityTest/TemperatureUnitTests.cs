using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QuantityMeasurementApp.Domain;

[TestClass]
public class UC14TemperatureTests
{
    private readonly TemperatureUnitHelper CELSIUS =
        new TemperatureUnitHelper(TemperatureUnit.CELSIUS);

    private readonly TemperatureUnitHelper FAHRENHEIT =
        new TemperatureUnitHelper(TemperatureUnit.FAHRENHEIT);

    private readonly TemperatureUnitHelper KELVIN =
        new TemperatureUnitHelper(TemperatureUnit.KELVIN);


    //  Equality Tests 

    [TestMethod]
    public void testTemperatureEquality_CelsiusToCelsius_SameValue()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(0, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(0, CELSIUS);

        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_FahrenheitToFahrenheit_SameValue()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(32, FAHRENHEIT);
        var t2 = new Quantity<TemperatureUnitHelper>(32, FAHRENHEIT);

        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(0, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(32, FAHRENHEIT);

        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(100, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(212, FAHRENHEIT);

        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(-40, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(-40, FAHRENHEIT);

        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void testTemperatureEquality_SymmetricProperty()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(0, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(32, FAHRENHEIT);

        Assert.IsTrue(t1.Equals(t2));
        Assert.IsTrue(t2.Equals(t1));
    }

    [TestMethod]
    public void testTemperatureEquality_ReflexiveProperty()
    {
        var t = new Quantity<TemperatureUnitHelper>(50, CELSIUS);

        Assert.IsTrue(t.Equals(t));
    }


    //  Conversion Tests 

    [TestMethod]
    public void testTemperatureConversion_CelsiusToFahrenheit_VariousValues()
    {
        double result = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(50));

        Assert.AreEqual(122, result, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_FahrenheitToCelsius_VariousValues()
    {
        double result = CELSIUS.ConvertFromBaseUnit(
            FAHRENHEIT.ConvertToBaseUnit(122));

        Assert.AreEqual(50, result, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_RoundTrip_PreservesValue()
    {
        double original = 75;

        double f = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(original));

        double back = CELSIUS.ConvertFromBaseUnit(
            FAHRENHEIT.ConvertToBaseUnit(f));

        Assert.AreEqual(original, back, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_SameUnit()
    {
        double value = CELSIUS.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(100));

        Assert.AreEqual(100, value, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_ZeroValue()
    {
        double result = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(0));

        Assert.AreEqual(32, result, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_NegativeValues()
    {
        double result = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(-20));

        Assert.AreEqual(-4, result, 0.001);
    }

    [TestMethod]
    public void testTemperatureConversion_LargeValues()
    {
        double result = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(1000));

        Assert.AreEqual(1832, result, 0.001);
    }

    // Cross Category Tests 

    [TestMethod]
    public void testTemperatureVsLengthIncompatibility()
    {
        var temp = new Quantity<TemperatureUnitHelper>(100, CELSIUS);
        var length = new Quantity<LengthUnitHelper>(100,
            new LengthUnitHelper(LengthUnit.FEET));

        Assert.IsFalse(temp.Equals(length));
    }

    [TestMethod]
    public void testTemperatureVsWeightIncompatibility()
    {
        var temp = new Quantity<TemperatureUnitHelper>(50, CELSIUS);
        var weight = new Quantity<WeightUnitHelper>(50,
            new WeightUnitHelper(WeightUnit.KILOGRAM));

        Assert.IsFalse(temp.Equals(weight));
    }

    [TestMethod]
    public void testTemperatureVsVolumeIncompatibility()
    {
        var temp = new Quantity<TemperatureUnitHelper>(25, CELSIUS);
        var volume = new Quantity<VolumeUnitHelper>(25,
            new VolumeUnitHelper(VolumeUnit.LITRE));

        Assert.IsFalse(temp.Equals(volume));
    }


    //  Enum Tests 

    [TestMethod]
    public void testTemperatureUnit_AllConstants()
    {
        Assert.IsNotNull(TemperatureUnit.CELSIUS);
        Assert.IsNotNull(TemperatureUnit.FAHRENHEIT);
        Assert.IsNotNull(TemperatureUnit.KELVIN);
    }

    [TestMethod]
    public void testTemperatureUnit_NameMethod()
    {
        Assert.AreEqual("CELSIUS", CELSIUS.GetUnitName());
    }

    [TestMethod]
    public void testTemperatureUnit_ConversionFactor()
    {
        Assert.AreEqual(1.0, CELSIUS.GetConversionFactor());
    }


    //  Validation 

    [TestMethod]
    public void testTemperatureNullOperandValidation_InComparison()
    {
        var t = new Quantity<TemperatureUnitHelper>(50, CELSIUS);

        Assert.IsFalse(t.Equals(null));
    }

    [TestMethod]
    public void testTemperatureDifferentValuesInequality()
    {
        var t1 = new Quantity<TemperatureUnitHelper>(50, CELSIUS);
        var t2 = new Quantity<TemperatureUnitHelper>(100, CELSIUS);

        Assert.IsFalse(t1.Equals(t2));
    }


    //  Precision 

    [TestMethod]
    public void testTemperatureConversionPrecision_Epsilon()
    {
        double result = FAHRENHEIT.ConvertFromBaseUnit(
            CELSIUS.ConvertToBaseUnit(37));

        Assert.AreEqual(98.6, result, 0.01);
    }

}