namespace Nodify.StateMachine
{
    public class TransitionViewModel : ObservableObject
    {
        private StateViewModel _source = default!;
        public StateViewModel Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private StateViewModel _target = default!;
        public StateViewModel Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }
    }
}
