using System.Windows;

namespace Nodify.StateMachine
{
    public class StateViewModel : ObservableObject
    {
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

        public bool IsEditable { get; set; } = true;
        public StateMachineViewModel Graph { get; internal set; } = default!;
        public NodifyObservableCollection<StateViewModel> Transitions { get; } = new NodifyObservableCollection<StateViewModel>();
    }
}
