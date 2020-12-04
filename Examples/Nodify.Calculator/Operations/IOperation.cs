namespace Nodify.Calculator
{
    public interface IOperation
    {
        decimal Execute(params decimal[] operands);
    }
}
