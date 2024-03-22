using Avalonia;
using Avalonia.Controls;

namespace Nodify
{
    public partial class Node
    {
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == HeaderProperty)
            {
                PseudoClasses.Set(":has-header", change.NewValue != null);
            }
        }
    }
}