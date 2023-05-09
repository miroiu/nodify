namespace Nodifier.Blueprint
{
    public interface IGraphMemento
    {
        IGraphSnapshot CreateSnapshot();
        void RestoreSnapshot(IGraphSnapshot snapshot);
    }

    public interface INodeMemento
    {
        INodeSnapshot CreateSnapshot();
        void RestoreSnapshot(INodeSnapshot snapshot);
    }

    public interface IMemento<TSnapshot>
    {
        TSnapshot CreateSnapshot();
        void RestoreSnapshot(TSnapshot snapshot);
    }
}
