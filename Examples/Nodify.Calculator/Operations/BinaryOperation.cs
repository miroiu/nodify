using System;

namespace Nodify.Calculator
{
    public class BinaryOperation : IOperation
    {
        private readonly Func<double, double, double> _func;

        public BinaryOperation(Func<double, double, double> func) => _func = func;

        public double Execute(params double[] operands)
            => _func.Invoke(operands[0], operands[1]);
    }
}
