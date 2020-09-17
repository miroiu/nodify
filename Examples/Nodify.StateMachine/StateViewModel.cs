using System;
using System.Windows;

namespace Nodify.StateMachine
{
    public class StateViewModel : ObservableObject
    {
        public Guid Id { get; }

        public StateViewModel(Guid id)
            => Id = id;

        public StateViewModel() : this(Guid.NewGuid()) { }

        // TODO: Can remove when auto layout is added
        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }

        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetProperty(ref _isRenaming, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private BlackboardItemReferenceViewModel? _actionReference;
        public BlackboardItemReferenceViewModel? ActionReference
        {
            get => _actionReference;
            set
            {
                if (SetProperty(ref _actionReference, value))
                {
                    SetAction(_actionReference);
                }
            }
        }

        public BlackboardItemViewModel? Action { get; private set; }

        public bool IsEditable { get; set; } = true;
        public StateMachineViewModel Graph { get; internal set; } = default!;
        public NodifyObservableCollection<StateViewModel> Transitions { get; } = new NodifyObservableCollection<StateViewModel>();

        private void SetAction(BlackboardItemReferenceViewModel? actionRef)
        {
            Action = BlackboardDescriptor.GetItem(actionRef);

            OnPropertyChanged(nameof(Action));
        }
    }
}
