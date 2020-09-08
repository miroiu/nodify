using System;
using System.Windows.Input;

namespace Nodify.StateMachine
{
    public class RequeryCommand : INodifyCommand
    {
        private readonly Action _action;
        private readonly Func<bool>? _condition;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RequeryCommand(Action action, Func<bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
            => _condition?.Invoke() ?? true;

        public void Execute(object parameter)
            => _action();

        public void RaiseCanExecuteChanged() { }
    }

    public class RequeryCommand<T> : INodifyCommand
    {
        private readonly Action<T> _action;
        private readonly Func<T, bool>? _condition;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RequeryCommand(Action<T> action, Func<T, bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T value)
            {
                return _condition?.Invoke(value) ?? true;
            }

            return _condition?.Invoke(default!) ?? true;
        }

        public void Execute(object parameter)
            => _action((T)parameter);

        public void RaiseCanExecuteChanged() { }
    }
}
