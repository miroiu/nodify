using System;
using System.Collections.Generic;
using System.Windows;

namespace Nodifier
{
    public interface IGraphWidget
    {
        IActionsHistory History { get; }

        IReadOnlyCollection<IGraphElement> Elements { get; }
        IReadOnlyCollection<IGraphElement> SelectedElements { get; }
        IReadOnlyCollection<IConnection> Connections { get; }
        IReadOnlyCollection<IGraphDecorator> Decorators { get; }

        IPendingConnection PendingConnection { get; }
        IEditorSettings Settings { get; }

        Point ViewportLocation { get; set; }
        double ViewportZoom { get; set; }
        Size ViewportSize { get; }
        Rect ElementsExtent { get; }
        Rect DecoratorsExtent { get; }

        void AddElement(IGraphElement node);
        void AddElements(IEnumerable<IGraphElement> nodes);
        void RemoveElement(IGraphElement node);
        void RemoveElements(IEnumerable<IGraphElement> nodes);

        void DeleteSelection();

        bool TryConnect(IConnector source, IConnector target);
        bool TryConnect(IConnector source, IGraphElement target);
        void Disconnect(IConnector connector);
        void RemoveConnection(IConnection connection);
        void AddConnection(IConnection connection);
        void Split(IConnection connection, Point location);

        void FocusLocation(Point location);
        void FitToScreen(Rect? area = null);
        void ZoomIn();
        void ZoomOut();

        void SelectAll();
        void SelectArea(Rect area);
        void UnselectAll();
        void UnselectArea(Rect area);

        /// <summary>Called when the view is loaded. Here you can access the actual size of the elements.</summary>
        event EventHandler Initialized;
    }
}