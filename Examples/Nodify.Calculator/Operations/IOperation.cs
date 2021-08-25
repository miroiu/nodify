namespace Nodify.Calculator
{
    public interface IOperation
    {
        double Execute(params double[] operands);
    }
}
