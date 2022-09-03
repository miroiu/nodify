using Nodify;
using Nodifier.Views;
using Stylet;
using System;
using System.Windows;
using System.Collections.Generic;

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

    public partial class Graph : PropertyChangedBase, IEditor, IViewAware
    {
        private NodifyEditor? _editor;
        protected NodifyEditor Editor => _editor ?? throw new InvalidOperationException($"No editor attached. Please implement {nameof(INodifyEditorAware)} in the view and wait for initialization.");
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

        public event EventHandler Initialized;

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

        private double _minViewportZoom = 0.1d;
        public double MinViewportZoom
        {
            get => _minViewportZoom;
            set => SetAndNotify(ref _minViewportZoom, value);
        }

        private double _maxViewportZoom = 2d;
        public double MaxViewportZoom
        {
            get => _maxViewportZoom;
            set => SetAndNotify(ref _maxViewportZoom, value);
        }

        private double _gridSnapSize;
        public double GridSnapSize
        {
            get => _gridSnapSize;
            set => SetAndNotify(ref _gridSnapSize, value);
        }

        private bool _disableZooming;
        public bool DisableZooming
        {
            get => _disableZooming;
            set => SetAndNotify(ref _disableZooming, value);
        }

        private bool _disablePanning;
        public bool DisablePanning
        {
            get => _disablePanning;
            set => SetAndNotify(ref _disablePanning, value);
        }

        private bool _disableAutoPanning;
        public bool DisableAutoPanning
        {
            get => _disableAutoPanning;
            set => SetAndNotify(ref _disableAutoPanning, value);
        }

        private bool _enableRealtimeSelection = true;
        public bool EnableRealtimeSelection
        {
            get => _enableRealtimeSelection;
            set => SetAndNotify(ref _enableRealtimeSelection, value);
        }

        void IViewAware.AttachView(UIElement view)
        {
            if (view is INodifyEditorAware editorAware)
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

        public virtual void FocusLocation(Point location) => Editor.BringIntoView(location);

        public virtual void SelectAll() => Editor.SelectAll();

        public virtual void ZoomIn() => Editor.ZoomIn();

        public virtual void ZoomOut() => Editor.ZoomOut();

        public virtual void SelectArea(Rect area) => Editor.SelectArea(area);

        public virtual void UnselectAll() => Editor.UnselectAll();

        public virtual void UnselectArea(Rect area) => Editor.UnselectArea(area);

        public virtual void AlignSelection(Alignment alignment)
        {
            EditorCommands.Alignment editorAlignment = _alignments[alignment];
            if (EditorCommands.Align.CanExecute(editorAlignment, Editor))
            {
                EditorCommands.Align.Execute(editorAlignment, Editor);
            }
        }
    }
}
