namespace Nodifier.Blueprint
{
    public interface IBlueprintNode
    {
        IGraphElement Widget { get; }
        IBlueprintGraph Graph { get; }

        IActionsHistory History { get; }
    }
}
