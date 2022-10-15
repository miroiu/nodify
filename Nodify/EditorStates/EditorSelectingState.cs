﻿using System.Windows.Input;
using static Nodify.SelectionHelper;

namespace Nodify
{
    /// <summary>The selecting state of the editor.</summary>
    public class EditorSelectingState : EditorState
    {
        private readonly SelectionType _type;

        /// <summary>The selection helper.</summary>
        protected SelectionHelper Selection { get; }

        /// <summary>Constructs an instance of the <see cref="EditorSelectingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorSelectingState(NodifyEditor editor, SelectionType type) : base(editor)
        {
            Selection = new SelectionHelper(editor);
            _type = type;
        }

        /// <inheritdoc />
        public override void Enter(EditorState? from)
            => Selection.Start(Editor.MouseLocation, _type);

        /// <inheritdoc />
        public override void Exit()
            => Selection.End();

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
            => Selection.Update(Editor.MouseLocation);

        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            if (EditorGestures.Pan.Matches(e.Source, e))
            {
                PushState(new EditorPanningState(Editor));
            }
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            if (EditorGestures.Select.Matches(e.Source, e))
            {
                PopState();
            }
        }

        /// <inheritdoc />
        public override void HandleAutoPanning(MouseEventArgs e)
            => HandleMouseMove(e);
    }
}
