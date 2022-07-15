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

    public partial class Graph : IEditorControls, IViewAware
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

        void IViewAware.AttachView(UIElement view)
        {
            if (view is INodifyEditorAware editorAware)
            {
                _editor = editorAware.Editor;
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
