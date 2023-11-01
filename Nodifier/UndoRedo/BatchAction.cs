using System.Collections.Generic;
using System.Linq;

namespace Nodifier
{
    public class BatchAction : IAction
    {
        public BatchAction(string? label, IEnumerable<IAction> history)
        {
            History = history.ToList();
            Label = label;
        }

        public IReadOnlyList<IAction> History { get; }

        public string? Label { get; }

        public void Apply()
        {
            for (int i = 0; i < History.Count; i++)
            {
                History[i].Apply();
            }
        }

        public void Unapply()
        {
            for (int i = History.Count - 1; i >= 0; i--)
            {
                History[i].Unapply();
            }
        }

        public override string? ToString()
            => Label;
    }
}
