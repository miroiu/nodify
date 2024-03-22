using Avalonia;
using Avalonia.Controls;

namespace Nodify
{
    public partial class PendingConnection
    {
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == IsVisibleProperty)
            {
                if (change.NewValue is bool isVisible)
                {
                    if (isVisible)
                    {
                        SetCurrentValue(Visual.IsVisibleProperty, true);
                        Opacity = 1;
                    }
                    else
                    {
                        ApplyTemplate();
                        // hacky: we need to wait for the template to be applied before we set visible to false otherwise
                        // the template will not be applied
                        if (Editor == null)
                        {
                            Opacity = 0;
                            return;
                        }
                        
                        SetCurrentValue(Visual.IsVisibleProperty, false);
                    }
                }
            }
        }
    }
}