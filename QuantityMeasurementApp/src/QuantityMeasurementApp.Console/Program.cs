using System;
using QuantityMeasurementApp.Domain;

/// <summary>
/// UC3 – Unified Quantity Measurement Application
/// Demonstrates length equality using generic Length class.
/// </summary>
public class Program
{
    // Generic equality method
    public static bool DemonstrateLengthEquality(Length length1, Length length2)
    {
        return length1.Equals(length2);
    }

    // Demonstrate Feet equality
    public static void DemonstrateFeetEquality()
    {
        Length length1 = new Length(1.0, Length.LengthUnit.FEET);
        Length length2 = new Length(1.0, Length.LengthUnit.FEET);

        Console.WriteLine("Feet equal? " + length1.Equals(length2));
    }

    // Demonstrate Inches equality
    public static void DemonstrateInchesEquality()
    {
        Length length1 = new Length(1.0, Length.LengthUnit.INCHES);
        Length length2 = new Length(1.0, Length.LengthUnit.INCHES);

        Console.WriteLine("Inches equal? " + length1.Equals(length2));
    }

    // Demonstrate Feet and Inches comparison
    public static void DemonstrateFeetInchesComparison()
    {
        Length length1 = new Length(1.0, Length.LengthUnit.FEET);
        Length length2 = new Length(12.0, Length.LengthUnit.INCHES);

        Console.WriteLine("Feet & Inches equal? " + length1.Equals(length2));
    }

    public static void Main(string[] args)
    {
        DemonstrateFeetEquality();
        DemonstrateInchesEquality();
        DemonstrateFeetInchesComparison();
    }
}
