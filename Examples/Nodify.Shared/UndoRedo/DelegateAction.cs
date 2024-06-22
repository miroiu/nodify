using System;

namespace Nodify.UndoRedo
{
    public class DelegateAction : IAction
    {
        private readonly Action _execute;
        private readonly Action _undo;

        public string? Label { get; }

        public DelegateAction(Action apply, Action unapply, string? label)
        {
            _execute = apply;
            _undo = unapply;
            Label = label;
        }

        public void Execute() => _execute();
        public void Undo() => _undo();

        public override string? ToString()
            => Label;
    }
}
