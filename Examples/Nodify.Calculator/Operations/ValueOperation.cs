using System;

namespace Nodify.Calculator
{
    public class ValueOperation : IOperation
    {
        private readonly Func<decimal> _func;

        public ValueOperation(Func<decimal> func) => _func = func;

        public decimal Execute(params decimal[] operands)
            => _func();
    }
}
