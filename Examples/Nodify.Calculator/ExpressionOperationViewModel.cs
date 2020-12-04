using StringMath;
using System.Linq;

namespace Nodify.Calculator
{
    public class ExpressionOperationViewModel : OperationViewModel
    {
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
                var operation = SMath.CreateOperation(Expression);
                var toRemove = Input.Where(i => !operation.Replacements.Contains(i.Title)).ToArray();
                toRemove.ForEach(i => Input.Remove(i));
                var left = Input.Select(s => s.Title).ToHashSet();

                foreach (var variable in operation.Replacements.Where(s => !left.Contains(s)))
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
                    var repl = new Replacements();
                    Input.ForEach(i => repl[i.Title] = i.Value);

                    Output.Value = SMath.Evaluate(Expression, repl);
                }
                catch
                {

                }
            }
        }
    }
}
