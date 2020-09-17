using System;

namespace Nodify.StateMachine
{
    public class BlackboardItemViewModel : ObservableObject
    {
        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private Type? _type;
        public Type? Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private NodifyObservableCollection<BlackboardKeyViewModel> _input = new NodifyObservableCollection<BlackboardKeyViewModel>();
        public NodifyObservableCollection<BlackboardKeyViewModel> Input
        {
            get => _input;
            set
            {
                if (value == null)
                {
                    value = new NodifyObservableCollection<BlackboardKeyViewModel>();
                }

                SetProperty(ref _input!, value);
            }
        }

        private NodifyObservableCollection<BlackboardKeyViewModel> _output = new NodifyObservableCollection<BlackboardKeyViewModel>();
        public NodifyObservableCollection<BlackboardKeyViewModel> Output
        {
            get => _output;
            set
            {
                if (value == null)
                {
                    value = new NodifyObservableCollection<BlackboardKeyViewModel>();
                }

                SetProperty(ref _output!, value);
            }
        }
    }
}
