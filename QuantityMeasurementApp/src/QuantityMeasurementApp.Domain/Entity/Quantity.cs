// UC- 10
// Generic Quantity class for LengthUnit And WeightUnit classes to implement the interface.
// Because in c# enum class can not implement the interfaces directly. so we have to use helper classes to use the interface. 
namespace QuantityMeasurementApp.Domain
{
    public class Quantity<T> where T : IMeasurable
    {
        private readonly double value;
        private readonly T unit;

        public double Value => value;
        public T Unit => unit;

        public Quantity(double value, T unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value");

            this.value = value;
            this.unit = unit;
        }

        public double ConvertTo(T targetUnit)
        {
            double baseValue = unit.ConvertToBaseUnit(value);

            return targetUnit.ConvertFromBaseUnit(baseValue);
        }

        public Quantity<T> Add(Quantity<T> other)
        {
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            double sumBase = base1 + base2;

            double result = unit.ConvertFromBaseUnit(sumBase);

            return new Quantity<T>(result, unit);
        }

        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            double sumBase = base1 + base2;

            double result = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<T>(result, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<T> other)
                return false;

            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            return Math.Abs(base1 - base2) < 0.0001;
        }

        public override int GetHashCode()
        {
            return unit.ConvertToBaseUnit(value).GetHashCode();
        }

        // SUBTRACTION 

        public Quantity<T> Subtract(Quantity<T> other)
        {
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            double diffBase = base1 - base2;

            double result = Math.Round(unit.ConvertFromBaseUnit(diffBase), 2);

            return new Quantity<T>(result, unit);
        }

        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            double diffBase = base1 - base2;

            double result = targetUnit.ConvertFromBaseUnit(diffBase);

            return new Quantity<T>(result, targetUnit);
        }

        // DIVIDE
        public double Divide(Quantity<T> other)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");
                
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            if (base2 == 0)
                throw new ArithmeticException("Division by zero");

            return base1 / base2;
        }

        public override string ToString()
        {
            return $"{value:F2} {unit.GetUnitName()}";
        }
    }
}