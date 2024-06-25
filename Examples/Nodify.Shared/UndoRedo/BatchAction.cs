using System.Collections.Generic;
using System.Linq;

namespace Nodify.UndoRedo
{
    public class BatchAction : IAction
    {
        public BatchAction(string? label, IEnumerable<IAction> history)
        {
            History = history.Reverse().ToList();
            Label = label;
        }

        public IReadOnlyList<IAction> History { get; }

        public string? Label { get; }

        public void Execute()
        {
            for (int i = History.Count - 1; i >= 0; i--)
            {
                History[i].Execute();
            }
        }

        public void Undo()
        {
            for (int i = 0; i < History.Count; i++)
            {
                History[i].Undo();
            }
        }

        public override string? ToString()
            => Label;
    }
}
