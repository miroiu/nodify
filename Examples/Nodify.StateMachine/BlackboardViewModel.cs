using System.Linq;

namespace Nodify.StateMachine
{
    public class BlackboardViewModel : ObservableObject
    {
        public NodifyObservableCollection<BlackboardKeyViewModel> Keys { get; } = new NodifyObservableCollection<BlackboardKeyViewModel>();

        public INodifyCommand AddKeyCommand { get; }

        public BlackboardViewModel()
        {
            Keys.WhenAdded(key =>
            {
                var existingKeyNames = Keys.Where(k => k != key).Select(k => k.Name).ToList();
                key.Name = existingKeyNames.GetUnique(key.Name ?? "New Key ");
            });

            AddKeyCommand = new DelegateCommand(() => Keys.Add(new BlackboardKeyViewModel
            {
                Name = "New Key "
            }));
        }
    }
}
