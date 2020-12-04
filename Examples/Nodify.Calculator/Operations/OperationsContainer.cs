using System;
using System.Linq;

namespace Nodify.Calculator
{
    public static class OperationsContainer
    {
        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static decimal Add(params decimal[] operands)
            => operands.Sum();

        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static decimal Multiply(params decimal[] operands)
            => operands.Aggregate((x, y) => x * y);

        public static decimal Divide(decimal a, decimal b)
            => a / b;

        public static decimal Subtract(decimal a, decimal b)
            => a - b;

        public static decimal Pow(decimal value, decimal exp)
            => (decimal)Math.Pow((double)value, (double)exp);

        [Operation(GenerateInputNames = false)]
        public static decimal Abs(decimal value)
            => Math.Abs(value);

        public static decimal PI()
            => (decimal)Math.PI;
    }

    public sealed class OperationAttribute : Attribute
    {
        public uint MaxInput { get; set; }
        public uint MinInput { get; set; }
        public bool GenerateInputNames { get; set; }
    }
}
