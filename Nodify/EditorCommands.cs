using System;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public static class EditorCommands
    {
        /// <summary>
        /// Zoom in relative to the editor's viewport center.
        /// </summary>
        public static RoutedUICommand ZoomIn { get; } = new RoutedUICommand("Zoom in", nameof(ZoomIn), typeof(EditorCommands), new InputGestureCollection
        {
           EditorGestures.Mappings.Editor.ZoomIn
        });

        /// <summary>
        /// Zoom out relative to the editor's viewport center.
        /// </summary>
        public static RoutedUICommand ZoomOut { get; } = new RoutedUICommand("Zoom out", nameof(ZoomOut), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.Mappings.Editor.ZoomOut
        });

        /// <summary>
        /// Select all <see cref="ItemContainer"/>s in the <see cref="NodifyEditor"/>.
        /// </summary>
        public static RoutedUICommand SelectAll { get; } = ApplicationCommands.SelectAll;

        /// <summary>
        /// Moves the <see cref="NodifyEditor.ViewportLocation"/> to the specified location.
        /// Parameter is a <see cref="Point"/> or a string that can be converted to a point.
        /// </summary>
        public static RoutedUICommand BringIntoView { get; } = new RoutedUICommand("Bring location into view", nameof(BringIntoView), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.Mappings.Editor.ResetViewportLocation
        });

        /// <summary>
        /// Scales the editor's viewport to fit all the <see cref="ItemContainer"/>s if that's possible.
        /// </summary>
        public static RoutedUICommand FitToScreen { get; } = new RoutedUICommand("Fit to screen", nameof(FitToScreen), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.Mappings.Editor.FitToScreen
        });

        /// <summary>
        /// Aligns the <see cref="NodifyEditor.SelectedContainers"/> using the specified alignment method.
        /// Parameter is of type <see cref="Alignment"/> or a string that can be converted to an alignment.
        /// </summary>
        public static RoutedUICommand Align { get; } = new RoutedUICommand("Align", nameof(Align), typeof(EditorCommands));

        internal static void Register(Type type)
        {
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(ZoomIn, OnZoomIn, OnQueryStatusZoomIn));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(ZoomOut, OnZoomOut, OnQueryStatusZoomOut));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(SelectAll, OnSelectAll, OnQuerySelectAllStatus));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(BringIntoView, OnBringIntoView, OnQueryBringIntoViewStatus));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(FitToScreen, OnFitToScreen, OnQueryFitToScreenStatus));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(Align, OnAlign, OnQueryAlignStatus));
        }

        private static void OnQueryAlignStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.SelectedContainersCount > 1;
            }
        }

        private static void OnAlign(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                if (e.Parameter is Alignment alignment)
                {
                    editor.AlignSelection(alignment, e.OriginalSource as ItemContainer);
                }
                else if (e.Parameter is string str && Enum.TryParse(str, true, out alignment))
                {
                    editor.AlignSelection(alignment, e.OriginalSource as ItemContainer);
                }
                else
                {
                    editor.AlignSelection(Alignment.Top, e.OriginalSource as ItemContainer);
                }
            }
        }

        private static void OnQueryBringIntoViewStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = !editor.DisablePanning;
            }
        }

        private static void OnBringIntoView(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                switch (e.Parameter)
                {
                    case Point location:
                        editor.BringIntoView(location);
                        break;
                    case string str:
                        editor.BringIntoView(Point.Parse(str));
                        break;
                    default:
                        editor.BringIntoView(new Point());
                        break;
                }
            }
        }

        private static void OnQueryFitToScreenStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.HasItems;
            }
        }

        private static void OnFitToScreen(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.FitToScreen();
            }
        }

        private static void OnQuerySelectAllStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = !editor.IsSelecting && editor.CanSelectMultipleItems;
            }
        }

        private static void OnSelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.SelectAll();
            }
        }

        private static void OnQueryStatusZoomIn(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.ViewportZoom < editor.MaxViewportZoom;
            }
        }

        private static void OnZoomIn(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.ZoomIn();
            }
        }

        private static void OnQueryStatusZoomOut(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.ViewportZoom > editor.MinViewportZoom;
            }
        }

        private static void OnZoomOut(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.ZoomOut();
            }
        }
    }
}
