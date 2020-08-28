using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nodify.Playground
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool SetProperty<T>(ref T reference, T value, [CallerMemberName] string propertyName = default!)
        {
            if (!Equals(reference, value))
            {
                reference = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

                return true;
            }

            return false;
        }
    }
}
