using System;
using QuantityMeasurementApp.Domain;

public static class Program
    {
        static void Main()
        {
            Console.WriteLine("UC7: Addition with Target Unit Specification\n");

            // 1 FEET + 12 INCHES -> FEET
            var r1 = QuantityMeasurement.demonstrateLengthAddition(new Length(1.0, LengthUnit.FEET), new Length(12.0, LengthUnit.INCH), LengthUnit.FEET);
            Console.WriteLine("1 FEET + 12 INCHES (FEET) = " + r1);


            // 1 FEET + 12 INCHES -> INCHES
            var r2 = QuantityMeasurement.demonstrateLengthAddition(new Length(1.0, LengthUnit.FEET),new Length(12.0, LengthUnit.INCH), LengthUnit.INCH);
            Console.WriteLine("1 FEET + 12 INCHES (INCHES) = " + r2);


            // 1 FEET + 12 INCHES -> YARDS
            var r3 = QuantityMeasurement.demonstrateLengthAddition(new Length(1.0, LengthUnit.FEET),new Length(12.0, LengthUnit.INCH), LengthUnit.YARD);
            Console.WriteLine("1 FEET + 12 INCHES (YARDS) = " + r3);


            // 1 YARD + 3 FEET -> YARDS
            var r4 = QuantityMeasurement.demonstrateLengthAddition(new Length(1.0, LengthUnit.YARD),new Length(3.0, LengthUnit.FEET), LengthUnit.YARD);
            Console.WriteLine("1 YARD + 3 FEET (YARDS) = " + r4);


            // 36 INCHES + 1 YARD -> FEET
            var r5 = QuantityMeasurement.demonstrateLengthAddition(new Length(36.0, LengthUnit.INCH),new Length(1.0, LengthUnit.YARD), LengthUnit.FEET);
            Console.WriteLine("36 INCHES + 1 YARD (FEET) = " + r5);


            // 2.54 CM + 1 INCH -> CM
            var r6 = QuantityMeasurement.demonstrateLengthAddition(new Length(2.54, LengthUnit.CENTIMETERS),new Length(1.0, LengthUnit.INCH), LengthUnit.CENTIMETERS);
            Console.WriteLine("2.54 CM + 1 INCH (CM) = " + r6);


            // 5 FEET + 0 INCHES -> YARDS
            var r7 = QuantityMeasurement.demonstrateLengthAddition(new Length(5.0, LengthUnit.FEET),new Length(0.0, LengthUnit.INCH), LengthUnit.YARD);
            Console.WriteLine("5 FEET + 0 INCHES (YARDS) = " + r7);


            // 5 FEET + (-2) FEET -> INCHES
            var r8 = QuantityMeasurement.demonstrateLengthAddition(new Length(5.0, LengthUnit.FEET),new Length(-2.0, LengthUnit.FEET), LengthUnit.INCH);
            Console.WriteLine("5 FEET + (-2) FEET (INCHES) = " + r8);
        }
    }
    