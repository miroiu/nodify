using System;

namespace Nodify.Calculator
{
    public class BinaryOperation : IOperation
    {
        private readonly Func<decimal, decimal, decimal> _func;

        public BinaryOperation(Func<decimal, decimal, decimal> func) => _func = func;

        public decimal Execute(params decimal[] operands)
            => _func.Invoke(operands[0], operands[1]);
    }
}
