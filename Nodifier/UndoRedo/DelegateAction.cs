using System;

namespace Nodifier
{
    public class DelegateAction : IAction
    {
        private readonly Action _apply;
        private readonly Action _unapply;

        public string? Label { get; }

        public DelegateAction(Action apply, Action unapply, string? label)
        {
            _apply = apply;
            _unapply = unapply;
            Label = label;
        }

        public void Apply() => _apply();
        public void Unapply() => _unapply();

        public override string? ToString()
            => Label;
    }
}
