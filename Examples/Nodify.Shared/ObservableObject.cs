using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nodify
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        public bool SetProperty<T>(ref T reference, T value, [CallerMemberName] in string propertyName = default!)
        {
            if (!Equals(reference, value))
            {
                reference = value;
                IsDirty = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
