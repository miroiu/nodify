namespace Nodifier.Blueprint
{
    public interface IMemento<TSnapshot>
    {
        void SaveSnapshot(TSnapshot snapshot);
        void RestoreSnapshot(TSnapshot snapshot);
    }
}
