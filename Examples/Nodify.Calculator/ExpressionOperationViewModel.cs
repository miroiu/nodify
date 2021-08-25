using StringMath;
using System.Collections.Generic;
using System.Linq;

namespace Nodify.Calculator
{
    public class ExpressionOperationViewModel : OperationViewModel
    {
        private readonly ICalculator _calculator = new StringMath.Calculator();

        private string? _expression;
        public string? Expression
        {
            get => _expression;
            set => SetProperty(ref _expression, value)
                .Then(GenerateInput);
        }

        private void GenerateInput()
        {
            try
            {
                OperationInfo? operation = _calculator.CreateOperation(Expression);
                ConnectorViewModel[]? toRemove = Input.Where(i => !operation.Variables.Contains(i.Title)).ToArray();
                toRemove.ForEach(i => Input.Remove(i));
                HashSet<string?> left = Input.Select(s => s.Title).ToHashSet();

                foreach (string variable in operation.Variables.Where(s => !left.Contains(s)))
                {
                    Input.Add(new ConnectorViewModel
                    {
                        Title = variable
                    });
                }

                OnInputValueChanged();
            }
            catch
            {

            }
        }

        protected override void OnInputValueChanged()
        {
            if (Output != null)
            {
                try
                {
                    Input.ForEach(i => _calculator.SetValue(i.Title, i.Value));
                    Output.Value = _calculator.Evaluate(Expression);
                }
                catch
                {

                }
            }
        }
    }
}
