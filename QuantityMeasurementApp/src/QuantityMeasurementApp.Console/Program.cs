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
    }
}