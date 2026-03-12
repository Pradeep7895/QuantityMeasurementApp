using System;
using QuantityMeasurementApp.Domain;
//using QuantityMeasurementApp.Services;

//UC- 10
class Program
{
    static void Main()
    {
        Console.WriteLine("=== LENGTH TESTS ===");

        var feet = new LengthUnitHelper(LengthUnit.FEET);
        var inch = new LengthUnitHelper(LengthUnit.INCH);

        var length1 = new Quantity<LengthUnitHelper>(1, feet);
        var length2 = new Quantity<LengthUnitHelper>(12, inch);

        Console.WriteLine($"1 FEET == 12 INCHES : {length1.Equals(length2)}");

        var converted = QuantityMeasurement.DemonstrateConversion(length1, inch);
        Console.WriteLine($"1 FEET -> INCHES = {converted.Value}");

        var sum = QuantityMeasurement.DemonstrateAddition(length1, length2);
        Console.WriteLine($"1 FEET + 12 INCHES = {sum.Value} {sum.Unit}");

        Console.WriteLine("\n=== WEIGHT TESTS ===");

        var kilogram = new WeightUnitHelper(WeightUnit.KILOGRAM);
        var gram = new WeightUnitHelper(WeightUnit.GRAM);

        var weight1 = new Quantity<WeightUnitHelper>(1, kilogram);
        var weight2 = new Quantity<WeightUnitHelper>(1000, gram);

        Console.WriteLine($"1 KG == 1000 GRAM : {weight1.Equals(weight2)}");

        var convertedWeight = QuantityMeasurement.DemonstrateConversion(weight1, gram);
        Console.WriteLine($"1 KG -> GRAM = {convertedWeight.Value}");

        //UC-11

        Console.WriteLine("===== UC11: Volume Measurements =====");

        var litre = new VolumeUnitHelper(VolumeUnit.LITRE);
        var ml = new VolumeUnitHelper(VolumeUnit.MILLILITRE);
        var gallon = new VolumeUnitHelper(VolumeUnit.GALLON);

        var volume1 = new Quantity<VolumeUnitHelper>(1.0, litre);
        var volume2 = new Quantity<VolumeUnitHelper>(1000.0, ml);
        var volume3 = new Quantity<VolumeUnitHelper>(1.0, gallon);

        // Equality
        Console.WriteLine("1 L == 1000 mL : " +
            QuantityMeasurement.DemonstrateEquality(volume1, volume2));

        // Conversion
        var convertedVol = QuantityMeasurement.DemonstrateConversion(volume1, ml);
        Console.WriteLine("1 L -> mL = " + convertedVol.Value);

        // Addition (implicit target)
        var sumVol = QuantityMeasurement.DemonstrateAddition(volume1, volume2);
        Console.WriteLine("1 L + 1000 mL = " + sumVol.Value + " " + sumVol.Unit);

        // Addition (explicit target)
        var sum2 = QuantityMeasurement.DemonstrateAddition(volume1, volume3, ml);
        Console.WriteLine("1 L + 1 Gallon in mL = " + sum2.Value + " " + sum2.Unit);

        // UC -12
        Console.WriteLine("===== UC12: Subtraction and Division =====");

        var feet2 = new LengthUnitHelper(LengthUnit.FEET);
        var inch2 = new LengthUnitHelper(LengthUnit.INCH);

        var length3 = new Quantity<LengthUnitHelper>(10.0, feet2);
        var length4 = new Quantity<LengthUnitHelper>(6.0, inch2);

        // Subtraction
        var result = QuantityMeasurement.DemonstrateSubtraction(length3, length4);

        Console.WriteLine($"10 FEET - 6 INCHES = {result}");

        // Subtraction with target unit
        var result2 = QuantityMeasurement.DemonstrateSubtraction(length3, length4, inch2);

        Console.WriteLine($"10 FEET - 6 INCHES = {result2}");

        // Division
        var ratio = QuantityMeasurement.DemonstrateDivision(length3,
            new Quantity<LengthUnitHelper>(2.0, feet2));

        Console.WriteLine($"10 FEET / 2 FEET = {ratio}");

        Console.WriteLine("\n===== Weight Example =====");

        var kg = new WeightUnitHelper(WeightUnit.KILOGRAM);
        var g = new WeightUnitHelper(WeightUnit.GRAM);

        var weight3 = new Quantity<WeightUnitHelper>(10.0, kg);
        var weight4 = new Quantity<WeightUnitHelper>(5000.0, g);

        var weightDiff = QuantityMeasurement.DemonstrateSubtraction(weight3, weight4);

        Console.WriteLine($"10 KG - 5000 G = {weightDiff}");

        Console.WriteLine("\n===== Volume Example =====");

        var litre2 = new VolumeUnitHelper(VolumeUnit.LITRE);
        var ml2 = new VolumeUnitHelper(VolumeUnit.MILLILITRE);

        var volume4 = new Quantity<VolumeUnitHelper>(5.0, litre2);
        var volume5 = new Quantity<VolumeUnitHelper>(500.0, ml2);

        var volumeDiff = QuantityMeasurement.DemonstrateSubtraction(volume4, volume5);

        Console.WriteLine($"5 L - 500 ML = {volumeDiff}");

        // UC- 14- Temperature unit
        Console.WriteLine("===== Temperature Demonstration =====");

        var celsius = new TemperatureUnitHelper(TemperatureUnit.CELSIUS);
        var fahrenheit = new TemperatureUnitHelper(TemperatureUnit.FAHRENHEIT);
        var kelvin = new TemperatureUnitHelper(TemperatureUnit.KELVIN);

        // Equality
        var t1 = new Quantity<TemperatureUnitHelper>(0, celsius);
        var t2 = new Quantity<TemperatureUnitHelper>(32, fahrenheit);
        var t3 = new Quantity<TemperatureUnitHelper>(273.15, kelvin);

        Console.WriteLine("0°C == 32°F : " + QuantityMeasurement.DemonstrateTemperatureEquality(t1, t2));

        Console.WriteLine("0°C == 273.15K : " + QuantityMeasurement.DemonstrateTemperatureEquality(t1, t3));


// Conversion
        var temp = new Quantity<TemperatureUnitHelper>(100, celsius);

        var convertedFeh = QuantityMeasurement.DemonstrateTemperatureConversion(temp, fahrenheit);

        Console.WriteLine("100°C = " + convertedFeh.Value + " °F");


        var convertedKelvin = QuantityMeasurement.DemonstrateTemperatureConversion(temp, kelvin);

        Console.WriteLine("100°C = " + convertedKelvin.Value + " K");


        // Unsupported arithmetic
        QuantityMeasurement.DemonstrateTemperatureArithmetic(t1, t1);
    }
}