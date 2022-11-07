using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Nodifier.XAML;

namespace Nodify.MinimalExample
{
    class ItemViewModel : ObservableObject
    {
        private Item _item { get; }

        public string _Text
        {
            get => _item.Text;
            set
            {
                _item.Text = value;
                OnPropertyChanged();
            }
        }

        public ItemViewModel(Item item)
        {
            _item = item;
        }
    }
    
    public class Item : TextBlock, IViewFor<ItemViewModel>
    {
        public Item(string Text)    
        {
            this.Text = Text;
        }
    }
    
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}