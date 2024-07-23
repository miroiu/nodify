namespace Nodify;

public partial class Minimap
{
    private Point viewportLocation;
    
    protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
    {
        recycleKey = null;
        return !IsItemItsOwnContainerOverride(item);
    }

    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return GetContainerForItemOverride() as Control;
    }
}