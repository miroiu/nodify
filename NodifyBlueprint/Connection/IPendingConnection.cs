namespace NodifyBlueprint
{
    public interface IPendingConnection
    {
        void Start(IConnector source);
        void Complete(object target);
    }
}
