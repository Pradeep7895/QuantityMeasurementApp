namespace QuantityMeasurementApp.Domain
{
    // UC-9 Weight class supports - Milligram, Gram, Kilogram, Pound, Tonne
    public class Weight
    {
        private readonly double value;
        private readonly WeightUnit unit;

        public double Value => value;
        public WeightUnit Unit => unit;

        public Weight(double value, WeightUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            this.value = value;
            this.unit = unit;
        }

        private double convertToBaseUnit()
        {
            return unit.convertToBaseUnit(value);
        }

        public bool Compare(Weight thatWeight)
        {
            if (thatWeight == null)
                return false;

            return this.convertToBaseUnit() == thatWeight.convertToBaseUnit();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Weight))
                return false;

            return Compare((Weight)obj);
        }

        public Weight convertTo(WeightUnit targetUnit)
        {
            double baseValue = convertToBaseUnit();
            double converted = targetUnit.convertFromBaseUnit(baseValue);

            return new Weight(converted, targetUnit);
        }

        public Weight add(Weight thatWeight)
        {
            if (thatWeight == null)
                throw new ArgumentException("Weight cannot be null.");

            double base1 = this.convertToBaseUnit();
            double base2 = thatWeight.convertToBaseUnit();

            double sumBase = base1 + base2;

            double resultValue = ConvertFromBaseToTargetUnit(sumBase, this.unit);

            return new Weight(resultValue, this.unit);
        }

        public Weight add(Weight weight, WeightUnit targetUnit)
        {
            if (weight == null)
                throw new ArgumentException("Weight cannot be null");

            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            return addAndConvert(weight, targetUnit);
        }

        private Weight addAndConvert(Weight weight, WeightUnit targetUnit)
        {
            double base1 = this.convertToBaseUnit();
            double base2 = weight.convertToBaseUnit();

            double sumBase = base1 + base2;

            double resultValue = ConvertFromBaseToTargetUnit(sumBase, targetUnit);

            return new Weight(resultValue, targetUnit);
        }

        private double ConvertFromBaseToTargetUnit(double baseValue, WeightUnit targetUnit)
        {
            return targetUnit.convertFromBaseUnit(baseValue);
        }

        public override int GetHashCode()
        {
            return convertToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"{value:F6} {unit}";
        }
    }
}