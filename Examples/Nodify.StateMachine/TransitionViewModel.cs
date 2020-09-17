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

        private BlackboardItemReferenceViewModel? _conditionReference;
        public BlackboardItemReferenceViewModel? ConditionReference
        {
            get => _conditionReference;
            set
            {
                if (SetProperty(ref _conditionReference, value))
                {
                    SetCondition(_conditionReference);
                }
            }
        }

        public BlackboardItemViewModel? Condition { get; private set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private void SetCondition(BlackboardItemReferenceViewModel? conditionRef)
        {
            Condition = BlackboardDescriptor.GetItem(conditionRef);

            OnPropertyChanged(nameof(Condition));
        }
    }
}
