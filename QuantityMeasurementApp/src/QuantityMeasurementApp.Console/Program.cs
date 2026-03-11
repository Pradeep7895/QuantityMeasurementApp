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
    }
}