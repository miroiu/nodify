using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nodify.UndoRedo
{
    public interface IActionsHistory : INotifyPropertyChanged
    {
        int MaxSize { get; set; }
        bool CanUndo { get; }
        bool CanRedo { get; }
        bool IsEnabled { get; set; }
        IAction? Current { get; }

        void Undo();
        void Redo();

        void Clear();

        /// <summary>
        /// All future modifications will be merged together to create a single history item until batch is disposed.
        /// </summary>
        IDisposable Batch(string? label = default);

        void Record(IAction action);

        /// <summary>
        /// All future modifications will be merged together to create a single history item until history is resumed.
        /// </summary>
        void Pause(string? label = default);

        /// <summary>Each future modifications will create a new history item.</summary>
        void Resume();
    }

    public interface IAction
    {
        string? Label { get; }

        void Execute();
        void Undo();
    }

    public static class ActionsHistoryExtensions
    {
        public static void Record(this IActionsHistory history, Action execute, Action unexecute, string? label = default)
            => history.Record(new DelegateAction(execute, unexecute, label));

        public static void ExecuteAction(this IActionsHistory history, IAction action)
        {
            history.Record(action);
            action.Execute();
        }
    }

    public class ActionsHistory : IActionsHistory
    {
        private readonly List<IAction> _history = new List<IAction>();
        private readonly List<IAction> _batchHistory = new List<IAction>();
        private int _position = -1;
        private bool _isApplyingOperation = false;
        private string? _batchLabel;
        private int _batchDepth;

        private static readonly PropertyChangedEventArgs _canRedoArgs = new PropertyChangedEventArgs(nameof(CanRedo));
        private static readonly PropertyChangedEventArgs _canUndoArgs = new PropertyChangedEventArgs(nameof(CanUndo));

        public event PropertyChangedEventHandler? PropertyChanged;
        public static readonly ActionsHistory Global = new ActionsHistory();

        public bool IsBatching { get; private set; }

        public int MaxSize { get; set; } = 50;

        public bool CanRedo => _history.Count > 0 && _position < _history.Count - 1;

        public bool CanUndo => _position > -1;

        public bool IsEnabled { get; set; } = true;

        public IAction? Current => CanUndo ? _history[_position] : null;

        public IDisposable Batch(string? label = default)
            => new BatchOperation(label, this);

        public void Record(IAction op)
        {
            // Prevent recording the undo or redo operation
            if (_isApplyingOperation || !IsEnabled)
            {
                return;
            }

            if (IsBatching)
            {
                _batchHistory.Add(op);
            }
            else
            {
                AddToUndoStack(op);
            }
        }

        private void AddToUndoStack(IAction op)
        {
            if (_position < _history.Count - 1)
            {
                _history.RemoveRange(_position + 1, _history.Count - _position - 1);
            }

            if (_history.Count >= MaxSize)
            {
                _history.RemoveAt(0);
                _position--;
            }

            _history.Add(op);
            _position++;

            PropertyChanged?.Invoke(this, _canRedoArgs);
            PropertyChanged?.Invoke(this, _canUndoArgs);
        }

        public void Undo()
        {
            if (IsBatching)
            {
                throw new InvalidOperationException($"{nameof(Undo)} is not allowed during a batch.");
            }

            if (CanUndo)
            {
                var op = _history[_position];
                _isApplyingOperation = true;
                op.Undo();
                _isApplyingOperation = false;
                _position--;
            }
        }

        public void Redo()
        {
            if (IsBatching)
            {
                throw new InvalidOperationException($"{nameof(Redo)} is not allowed during a batch.");
            }

            if (CanRedo)
            {
                _position++;
                var op = _history[_position];
                _isApplyingOperation = true;
                op.Execute();
                _isApplyingOperation = false;
            }
        }

        public void Clear()
        {
            _history.Clear();
            _batchHistory.Clear();
        }

        public void Pause(string? label = default)
        {
            if (_batchDepth > 0)
            {
                return;
            }

            _batchLabel = label;
            IsBatching = true;
        }

        public void Resume()
        {
            if (_batchDepth > 0)
            {
                return;
            }

            if (_batchHistory.Count > 0)
            {
                AddToUndoStack(new BatchAction(_batchLabel, _batchHistory));
                _batchHistory.Clear();
            }

            _batchLabel = null;
            IsBatching = false;
        }

        private class BatchOperation : IDisposable
        {
            private readonly ActionsHistory _history;
            private bool _disposed;

            public BatchOperation(string? label, ActionsHistory history)
            {
                _history = history;
                _history.Pause(label);
                _history._batchDepth++;
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _disposed = true;
                    _history._batchDepth--;
                    _history.Resume();
                }
            }
        }
    }
}
