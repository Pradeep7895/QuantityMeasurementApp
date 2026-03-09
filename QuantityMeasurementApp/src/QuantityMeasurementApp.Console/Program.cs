using System;
using QuantityMeasurementApp.Domain;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    public class Program
    {
        static void Main()
        {
            var w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000.0, WeightUnit.GRAM);

            Console.WriteLine("Equality: " + WeightMeasurement.demonstrateWeightEquality(w1, w2));

            var converted = WeightMeasurement.demonstrateWeightConversion(new Weight(2.0, WeightUnit.POUND), WeightUnit.KILOGRAM);

            Console.WriteLine("Converted: " + converted);

            var sum = WeightMeasurement.demonstrateWeightAddition(w1, w2);

            Console.WriteLine("Addition: " + sum);
        }
    }
}