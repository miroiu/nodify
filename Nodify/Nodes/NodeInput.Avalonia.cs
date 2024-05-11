namespace Nodify;

public partial class NodeInput
{
    /// <inheritdoc />
    public NodeInput()
    {
        UpdatePseudoClasses();
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == OrientationProperty)
            UpdatePseudoClasses();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set("vertial", Orientation == Orientation.Vertical);
    }
}