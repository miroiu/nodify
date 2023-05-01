using Nodify;
using Nodifier.Views;
using System;
using System.Windows;
using System.Collections.Generic;
using Nodifier.XAML;

namespace Nodifier
{
    public enum Alignment
    {
        Top,
        Left,
        Bottom,
        Right,
        Middle,
        Center
    }

    public partial class GraphWidget : IGraphWidget, IViewAware
    {
        private NodifyEditor? _editor;
        protected NodifyEditor Editor => _editor ?? throw new GraphException($"No editor attached. Please implement {nameof(IEditorHost)} in the view and wait for initialization.");
        UIElement? IViewAware.View => _editor;

        private static readonly Dictionary<Alignment, EditorCommands.Alignment> _alignments = new Dictionary<Alignment, EditorCommands.Alignment>
        {
            { Alignment.Top, EditorCommands.Alignment.Top },
            { Alignment.Left, EditorCommands.Alignment.Left },
            { Alignment.Bottom, EditorCommands.Alignment.Bottom },
            { Alignment.Right, EditorCommands.Alignment.Right },
            { Alignment.Middle, EditorCommands.Alignment.Middle },
            { Alignment.Center, EditorCommands.Alignment.Center }
        };

        public event EventHandler? Initialized;
        public IEditorSettings Settings { get; } = new EditorSettings();

        private Point _viewportLocation;
        public Point ViewportLocation
        {
            get => _viewportLocation;
            set => SetAndNotify(ref _viewportLocation, value);
        }

        private Size _viewportSize;
        public Size ViewportSize
        {
            get => _viewportSize;
            set => SetAndNotify(ref _viewportSize, value);
        }

        private double _viewportZoom = 1d;
        public double ViewportZoom
        {
            get => _viewportZoom;
            set => SetAndNotify(ref _viewportZoom, value);
        }

        private Rect _elementsExtent;
        public Rect ElementsExtent
        {
            get => _elementsExtent;
            set => SetAndNotify(ref _elementsExtent, value);
        }

        private Rect _decoratorsExtent;
        public Rect DecoratorsExtent
        {
            get => _decoratorsExtent;
            set => SetAndNotify(ref _decoratorsExtent, value);
        }

        void IViewAware.AttachView(UIElement view)
        {
            if (view is IEditorHost editorAware)
            {
                _editor = editorAware.Editor;
            }

            if (view is FrameworkElement elem)
            {
                if (elem.IsLoaded)
                {
                    Initialized?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    elem.Loaded += (o, e) => Initialized?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void FocusLocation(Point location) => Editor.BringIntoView(location, animated: true);

        public void FitToScreen(Rect? area = null) => Editor.FitToScreen(area);

        public void SelectAll() => Editor.SelectAll();

        public void ZoomIn() => Editor.ZoomIn();

        public void ZoomOut() => Editor.ZoomOut();

        public void SelectArea(Rect area) => Editor.SelectArea(area);

        public void UnselectAll() => Editor.UnselectAll();

        public void UnselectArea(Rect area) => Editor.UnselectArea(area);

        public void AlignSelection(Alignment alignment)
        {
            EditorCommands.Alignment editorAlignment = _alignments[alignment];
            if (EditorCommands.Align.CanExecute(editorAlignment, Editor))
            {
                EditorCommands.Align.Execute(editorAlignment, Editor);
            }
        }

        protected void OnDragStarted()
            => History.Pause("Dragging");

        protected void OnDragCompleted()
            => History.Resume();

        protected void OnSelectStarted()
            => History.Pause("Selecting");

        protected void OnSelectCompleted()
            => History.Resume();
    }
}
