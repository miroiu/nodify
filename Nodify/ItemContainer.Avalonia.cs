using Avalonia;
using Avalonia.Controls;

namespace Nodify
{
    public partial class ItemContainer
    {
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == IsPreviewingSelectionProperty)
            {
                UpdatePseudoClasses();
            }
        }

        private void UpdatePseudoClasses()
        {
            PseudoClasses.Set(":previewing-selection", IsPreviewingSelection == true);
            PseudoClasses.Set(":not-previewing-selection", IsPreviewingSelection == false);
            PseudoClasses.Set(":null-previewing-selection", IsPreviewingSelection == null);
        }
    }
}