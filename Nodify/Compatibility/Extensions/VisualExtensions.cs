namespace Nodify.Compatibility;

internal static class VisualExtensions
{
    public static bool IsAncestorOf(this Visual visual, Visual target)
    {
        return ReferenceEquals(visual, target) || visual.IsVisualAncestorOf(target);
    }
}