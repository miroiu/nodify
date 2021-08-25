using System;

namespace Nodify.Calculator
{
    public class ValueOperation : IOperation
    {
        private readonly Func<double> _func;

        public ValueOperation(Func<double> func) => _func = func;

        public double Execute(params double[] operands)
            => _func();
    }
}
