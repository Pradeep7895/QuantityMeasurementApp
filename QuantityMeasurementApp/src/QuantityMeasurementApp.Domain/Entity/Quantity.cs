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

        // ENUM FOR ARITHMETIC OPERATIONS
        // UC - 13

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        // ADD METHODS

        public Quantity<T> Add(Quantity<T> other)
        {
            validateArithmeticOperands(other, unit, true);

            double baseResult = performArithmetic(other, unit, ArithmeticOperation.ADD);

            double result = unit.ConvertFromBaseUnit(baseResult);

            return new Quantity<T>(Round(result), unit);
        }

        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            validateArithmeticOperands(other, targetUnit, true);

            double baseResult = performArithmetic(other, targetUnit, ArithmeticOperation.ADD);

            double result = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<T>(Round(result), targetUnit);
        }

        // SUBTRACT METHODS

        public Quantity<T> Subtract(Quantity<T> other)
        {
            validateArithmeticOperands(other, unit, true);

            double baseResult = performArithmetic(other, unit, ArithmeticOperation.SUBTRACT);

            double result = unit.ConvertFromBaseUnit(baseResult);

            return new Quantity<T>(Round(result), unit);
        }

        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            validateArithmeticOperands(other, targetUnit, true);

            double baseResult = performArithmetic(other, targetUnit, ArithmeticOperation.SUBTRACT);

            double result = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<T>(Round(result), targetUnit);
        }

        // DIVIDE METHOD

        public double Divide(Quantity<T> other)
        {
            validateArithmeticOperands(other, default(T), false);

            return performArithmetic(other, default(T), ArithmeticOperation.DIVIDE);
        }

           // CENTRALIZED ARITHMETIC METHOD

        private double performArithmetic(Quantity<T> other, T targetUnit, ArithmeticOperation operation)
        {
            double base1 = unit.ConvertToBaseUnit(value);
            double base2 = other.unit.ConvertToBaseUnit(other.value);

            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return base1 + base2;

                case ArithmeticOperation.SUBTRACT:
                    return base1 - base2;

                case ArithmeticOperation.DIVIDE:

                    if (base2 == 0)
                        throw new ArithmeticException("Division by zero");

                    return base1 / base2;

                default:
                    throw new InvalidOperationException("Unsupported operation");
            }
        }

        // CENTRAL VALIDATION METHOD

        private void validateArithmeticOperands(Quantity<T> other, T targetUnit, bool targetUnitRequired)
        {
            if (other == null)
                throw new ArgumentException("Operand quantity cannot be null");

            if (!unit.GetType().Equals(other.unit.GetType()))
                throw new ArgumentException("Incompatible measurement categories");

            if (!double.IsFinite(value) || !double.IsFinite(other.value))
                throw new ArgumentException("Invalid numeric values");

            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");
        }

        // ROUNDING METHOD

        private double Round(double val)
        {
            return Math.Round(val, 2);
        }

        public override string ToString()
        {
            return $"{value:F2} {unit.GetUnitName()}";
        }
    }
}