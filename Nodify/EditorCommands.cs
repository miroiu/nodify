using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    internal static class EditorCommands
    {
        /// <summary>
        /// Specifies the possible alignment values used by the <see cref="Align"/> command.
        /// </summary>
        public enum Alignment
        {
            Top,
            Left,
            Bottom,
            Right,
            Middle,
            Center
        }

        /// <summary>
        /// Zoom in relative to the editor's viewport center.
        /// </summary>
        public static RoutedUICommand ZoomIn { get; } = new RoutedUICommand("Zoom in", nameof(ZoomIn), typeof(EditorCommands), new InputGestureCollection
        {
           EditorGestures.ZoomIn 
        });

        /// <summary>
        /// Zoom out relative to the editor's viewport center.
        /// </summary>
        public static RoutedUICommand ZoomOut { get; } = new RoutedUICommand("Zoom out", nameof(ZoomOut), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.ZoomOut
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
            EditorGestures.ResetViewportLocation
        });

        /// <summary>
        /// Scales the editor's viewport to fit all the <see cref="ItemContainer"/>s if that's possible.
        /// </summary>
        public static RoutedUICommand FitToScreen { get; } = new RoutedUICommand("Fit to screen", nameof(FitToScreen), typeof(EditorCommands), new InputGestureCollection
        {
            EditorGestures.FitToScreen
        });

        /// <summary>
        /// Aligns <see cref="NodifyEditor.SelectedItems"/> using the specified alignment method.
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

        private static void OnQueryAlignStatus(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.HasManyItemsSelected;
            }
        }

        private static void OnAlign(object? sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                var selected = editor.InternalSelectedItems;
                if (selected.Count > 0)
                {
                    if (editor.ItemsDragStartedCommand?.CanExecute(null) ?? false)
                    {
                        editor.ItemsDragStartedCommand.Execute(null);
                    }

                    var containers = new List<ItemContainer>(selected.Count);

                    for (var i = 0; i < selected.Count; i++)
                    {
                        containers.Add((ItemContainer)editor.ContainerFromItem(selected[i]));
                    }

                    if (e.Parameter is Alignment alignment)
                    {
                        AlignContainers(containers, alignment, e.Source as ItemContainer);
                    }
                    else if (e.Parameter is string str && Enum.TryParse(str, true, out alignment))
                    {
                        AlignContainers(containers, alignment, e.Source as ItemContainer);
                    }
                    else
                    {
                        AlignContainers(containers, Alignment.Top, e.Source as ItemContainer);
                    }

                    if (editor.ItemsDragCompletedCommand?.CanExecute(null) ?? false)
                    {
                        editor.ItemsDragCompletedCommand.Execute(null);
                    }
                }
            }
        }

        private static void AlignContainers(List<ItemContainer> containers, Alignment alignment, ItemContainer? instigator = default)
        {
            switch (alignment)
            {
                case Alignment.Top:
                    double top = instigator?.Location.Y ?? containers.Min(x => x.Location.Y);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, top));
                    break;

                case Alignment.Left:
                    double left = instigator?.Location.X ?? containers.Min(x => x.Location.X);
                    containers.ForEach(c => c.Location = new Point(left, c.Location.Y));
                    break;

                case Alignment.Bottom:
                    double bottom = instigator != null ? instigator.Location.Y + instigator.Bounds.Height : containers.Max(x => x.Location.Y + x.Bounds.Height);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, bottom - c.Bounds.Height));
                    break;

                case Alignment.Right:
                    double right = instigator != null ? instigator.Location.X + instigator.Bounds.Width : containers.Max(x => x.Location.X + x.Bounds.Width);
                    containers.ForEach(c => c.Location = new Point(right - c.Bounds.Width, c.Location.Y));
                    break;

                case Alignment.Middle:
                    double mid = instigator != null ? instigator.Location.Y + instigator.Bounds.Height / 2 : containers.Average(c => c.Location.Y + c.Bounds.Height / 2);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, mid - c.Bounds.Height / 2));
                    break;

                case Alignment.Center:
                    double center = instigator != null ? instigator.Location.X + instigator.Bounds.Width / 2 : containers.Average(c => c.Location.X + c.Bounds.Width / 2);
                    containers.ForEach(c => c.Location = new Point(center - c.Bounds.Width / 2, c.Location.Y));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
            }
        }

        private static void OnQueryBringIntoViewStatus(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = !editor.DisablePanning;
            }
        }

        private static void OnBringIntoView(object? sender, ExecutedRoutedEventArgs e)
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

        private static void OnQueryFitToScreenStatus(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.HasItems;
            }
        }

        private static void OnFitToScreen(object? sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.FitToScreen();
            }
        }

        private static void OnQuerySelectAllStatus(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = !editor.IsSelecting;
            }
        }

        private static void OnSelectAll(object? sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.SelectAll();
            }
        }

        private static void OnQueryStatusZoomIn(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.ViewportZoom < editor.MaxViewportZoom;
            }
        }

        private static void OnZoomIn(object? sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.ZoomIn();
            }
        }

        private static void OnQueryStatusZoomOut(object? sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.ViewportZoom > editor.MinViewportZoom;
            }
        }

        private static void OnZoomOut(object? sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                editor.ZoomOut();
            }
        }
    }
}
