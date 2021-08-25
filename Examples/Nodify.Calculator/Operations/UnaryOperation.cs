using System;

namespace Nodify.Calculator
{
    public class UnaryOperation : IOperation
    {
        private readonly Func<double, double> _func;

        public UnaryOperation(Func<double, double> func) => _func = func;

        public double Execute(params double[] operands)
            => _func.Invoke(operands[0]);
    }
}
