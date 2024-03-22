namespace Nodify.Compatibility;

public class MultiSelector : SelectingItemsControl
{
    public MultiSelector()
    {
        Selection.SelectionChanged += OnSelectionChanged;
        SelectionMode = SelectionMode.Multiple;
    }

    private void OnSelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs e)
    {
        OnSelectionChanged(e);
    }
    
    protected virtual void OnSelectionChanged(SelectionModelSelectionChangedEventArgs e) { }

    public bool CanSelectMultipleItems
    {
        get => SelectionMode == SelectionMode.Multiple; 
        set => SelectionMode = value ? SelectionMode.Multiple : SelectionMode.Single;
    }

    protected void BeginUpdateSelectedItems()
    {
        Selection.BeginBatchUpdate();
    }
    
    protected void EndUpdateSelectedItems()
    {
        Selection.EndBatchUpdate();
    }
    
    public void UnselectAll()
    {
        Selection.Clear();
    }

    public bool HasItems => Items.Count > 0;
        
    public bool HasManyItemsSelected => Selection.SelectedItems.Count > 1;
    
    public IReadOnlyList<object?> InternalSelectedItems => Selection.SelectedItems;

    public void SelectAll()
    {
        Selection.SelectAll();
    }

    protected virtual DependencyObject GetContainerForItemOverride()
    {
        return new ContentPresenter();
    }

    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return (GetContainerForItemOverride() as Control)!;
    }
    
    protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
    {
        recycleKey = null;
        return !IsItemItsOwnContainerOverride(item);
    }

    protected virtual bool IsItemItsOwnContainerOverride(object item)
    {
        return false;
    }
}