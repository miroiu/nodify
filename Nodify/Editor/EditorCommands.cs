using Nodify.Interactivity;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Provides common commands for the <see cref="NodifyEditor"/>.
    /// </summary>
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
        public static RoutedUICommand SelectAll { get; } = new RoutedUICommand(ApplicationCommands.SelectAll.Text, nameof(SelectAll), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.Mappings.Editor.SelectAll
        });

        /// <summary>
        /// Moves the <see cref="NodifyEditor.ViewportLocation"/> to the specified location.
        /// Parameter is a <see cref="Point"/> or a string that can be converted to a point.
        /// </summary>
        public static RoutedUICommand BringIntoView { get; } = new RoutedUICommand("Bring location into view", nameof(BringIntoView), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.Mappings.Editor.ResetViewport
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

        /// <summary>
        /// Locks the position of the <see cref="NodifyEditor.SelectedContainers"/>.
        /// </summary>
        public static RoutedUICommand LockSelection { get; } = new RoutedUICommand("Lock selection", nameof(LockSelection), typeof(EditorCommands));

        /// <summary>
        /// Unlocks the position of the <see cref="NodifyEditor.SelectedContainers"/>.
        /// </summary>
        public static RoutedUICommand UnlockSelection { get; } = new RoutedUICommand("Unlock selection", nameof(UnlockSelection), typeof(EditorCommands));

        internal static void RegisterCommandBindings<T>()
        {
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(ZoomIn, OnZoomIn, OnQueryStatusZoomIn));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(ZoomOut, OnZoomOut, OnQueryStatusZoomOut));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(SelectAll, OnSelectAll, OnQuerySelectAllStatus));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(BringIntoView, OnBringIntoView, OnQueryBringIntoViewStatus));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(FitToScreen, OnFitToScreen, OnQueryFitToScreenStatus));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(Align, OnAlign, OnQueryAlignStatus));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(LockSelection, OnLock, OnQueryLockStatus));
            CommandManager.RegisterClassCommandBinding(typeof(T), new CommandBinding(UnlockSelection, OnUnlock, OnQueryUnlockStatus));
        }

        private static void OnQueryLockStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.SelectedContainers.Any(x => x.IsDraggable);
            }
        }

        private static void OnLock(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.LockSelection();
            }
        }

        private static void OnQueryUnlockStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.SelectedContainers.Any(x => !x.IsDraggable);
            }
        }

        private static void OnUnlock(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.UnlockSelection();
            }
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
                        editor.ResetViewport();
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
