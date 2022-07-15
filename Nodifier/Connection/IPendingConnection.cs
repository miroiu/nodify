using System.Windows;

namespace Nodifier
{
    public interface IPendingConnection
    {
        bool IsVisible { get; set; }
        Point TargetLocation { get; set; }
        public object? PreviewTarget { get; set; }

        void Start(IConnector source);
        void Complete(object target);
    }
}
