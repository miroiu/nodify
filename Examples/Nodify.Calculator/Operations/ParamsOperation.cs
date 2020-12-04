using System;

namespace Nodify.Calculator
{
    public class ParamsOperation : IOperation
    {
        private readonly Func<decimal[], decimal> _func;

        public ParamsOperation(Func<decimal[], decimal> func) => _func = func;

        public decimal Execute(params decimal[] operands)
            => _func.Invoke(operands);
    }
}
