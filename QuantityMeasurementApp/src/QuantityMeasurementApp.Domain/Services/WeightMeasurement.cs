using QuantityMeasurementApp.Domain;

namespace QuantityMeasurementApp.Services
{
    public class WeightMeasurement
    {
        // WEIGHT METHODS

        public static bool demonstrateWeightEquality(Weight weight1, Weight weight2)
        {
            return weight1.Equals(weight2);
        }

        public static bool demonstrateWeightComparison(double value1, WeightUnit unit1, double value2, WeightUnit unit2)
        {
            var w1 = new Weight(value1, unit1);
            var w2 = new Weight(value2, unit2);

            return w1.Equals(w2);
        }

        public static Weight demonstrateWeightConversion(double value, WeightUnit fromUnit, WeightUnit toUnit)
        {
            var weight = new Weight(value, fromUnit);
            return weight.convertTo(toUnit);
        }

        public static Weight demonstrateWeightConversion(Weight weight, WeightUnit toUnit)
        {
            return weight.convertTo(toUnit);
        }

        public static Weight demonstrateWeightAddition(Weight weight1, Weight weight2)
        {
            return weight1.add(weight2);
        }

        public static Weight demonstrateWeightAddition(Weight weight1, Weight weight2, WeightUnit targetUnit)
        {
            return weight1.add(weight2, targetUnit);
        }
    }
}