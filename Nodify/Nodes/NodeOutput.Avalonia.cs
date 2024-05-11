namespace Nodify;

public partial class NodeOutput
{
    /// <inheritdoc />
    public NodeOutput()
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