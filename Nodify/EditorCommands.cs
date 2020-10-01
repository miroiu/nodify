using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    public static class EditorCommands
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
        /// Zoom in relative to the <see cref="NodifyEditor.Viewport"/>'s center.
        /// </summary>
        public static RoutedUICommand ZoomIn { get; } = new RoutedUICommand("Zoom in", nameof(ZoomIn), typeof(EditorCommands), new InputGestureCollection
        {
            new KeyGesture(Key.OemPlus, ModifierKeys.Control),
            new KeyGesture(Key.Add, ModifierKeys.Control)
        });

        /// <summary>
        /// Zoom out relative to the <see cref="NodifyEditor.Viewport"/>'s center.
        /// </summary>
        public static RoutedUICommand ZoomOut { get; } = new RoutedUICommand("Zoom out", nameof(ZoomOut), typeof(EditorCommands), new InputGestureCollection
        {
            new KeyGesture(Key.OemMinus, ModifierKeys.Control),
            new KeyGesture(Key.Subtract, ModifierKeys.Control)
        });

        /// <summary>
        /// Select all <see cref="ItemContainer"/>s in the <see cref="NodifyEditor"/>.
        /// </summary>
        public static RoutedUICommand SelectAll { get; } = new RoutedUICommand("Select all", nameof(SelectAll), typeof(EditorCommands), new InputGestureCollection
        {
            new KeyGesture(Key.A, ModifierKeys.Control)
        });

        /// <summary>
        /// Delete <see cref="NodifyEditor.SelectedItems"/> if the ItemsSource is not bound.
        /// </summary>
        public static RoutedUICommand Delete { get; } = new RoutedUICommand("Delete", nameof(Delete), typeof(EditorCommands), new InputGestureCollection
        {
            new KeyGesture(Key.Delete)
        });

        /// <summary>
        /// Moves the <see cref="NodifyEditor.Viewport"/> to the specified location.
        /// Parameter is a <see cref="Point"/> or a string that can be converted to a point.
        /// </summary>
        public static RoutedUICommand BringIntoView { get; } = new RoutedUICommand("Bring location into view", nameof(BringIntoView), typeof(EditorCommands));

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
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(Delete, OnDelete, OnQueryDeleteStatus));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(BringIntoView, OnBringIntoView, OnQueryBringIntoViewStatus));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(Align, OnAlign, OnQueryAlignStatus));
        }

        private static void OnQueryAlignStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = ((MultiSelector)editor).SelectedItems.Count > 1;
            }
        }

        private static void OnAlign(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                var selected = ((MultiSelector)editor).SelectedItems;
                if (selected.Count > 0)
                {
                    if (editor.ItemsDragStartedCommand?.CanExecute(null) ?? false)
                    {
                        editor.ItemsDragStartedCommand.Execute(null);
                    }

                    var containers = new List<ItemContainer>(selected.Count);

                    for (int i = 0; i < selected.Count; i++)
                    {
                        containers.Add((ItemContainer)editor.ItemContainerGenerator.ContainerFromItem(selected[i]));
                    }

                    if (e.Parameter is Alignment alignment)
                    {
                        AlignContainers(containers, alignment, e.OriginalSource as ItemContainer);
                    }
                    else if (e.Parameter is string str && Enum.TryParse(str, true, out alignment))
                    {
                        AlignContainers(containers, alignment, e.OriginalSource as ItemContainer);
                    }
                    else
                    {
                        AlignContainers(containers, Alignment.Top, e.OriginalSource as ItemContainer);
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
                    var top = instigator?.Location.Y ?? containers.Min(x => x.Location.Y);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, top));
                    break;

                case Alignment.Left:
                    var left = instigator?.Location.X ?? containers.Min(x => x.Location.X);
                    containers.ForEach(c => c.Location = new Point(left, c.Location.Y));
                    break;

                case Alignment.Bottom:
                    var bottom = instigator != null ? instigator.Location.Y + instigator.ActualHeight : containers.Max(x => x.Location.Y + x.ActualHeight);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, bottom - c.ActualHeight));
                    break;

                case Alignment.Right:
                    var right = instigator != null ? instigator.Location.X + instigator.ActualWidth : containers.Max(x => x.Location.X + x.ActualWidth);
                    containers.ForEach(c => c.Location = new Point(right - c.ActualWidth, c.Location.Y));
                    break;

                case Alignment.Middle:
                    var mid = instigator != null ? instigator.Location.Y + instigator.ActualHeight / 2 : containers.Average(c => c.Location.Y + c.ActualHeight / 2);
                    containers.ForEach(c => c.Location = new Point(c.Location.X, mid - c.ActualHeight / 2));
                    break;

                case Alignment.Center:
                    var center = instigator != null ? instigator.Location.X + instigator.ActualWidth / 2 : containers.Average(c => c.Location.X + c.ActualWidth / 2);
                    containers.ForEach(c => c.Location = new Point(center - c.ActualWidth / 2, c.Location.Y));
                    break;
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
                if (e.Parameter is Point location)
                {
                    editor.BringIntoView(location);
                }
                else if (e.Parameter is string str)
                {
                    editor.BringIntoView(Point.Parse(str));
                }
                else
                {
                    editor.BringIntoView(new Point());
                }
            }
        }

        private static void OnQueryDeleteStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = editor.ItemsSource == null && ((MultiSelector)editor).SelectedItems.Count > 0;
            }
        }

        private static void OnDelete(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                var items = new ArrayList(((MultiSelector)editor).SelectedItems);
                foreach (var item in items)
                {
                    editor.Items.Remove(item);
                }
            }
        }

        private static void OnQuerySelectAllStatus(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is NodifyEditor editor)
            {
                e.CanExecute = !editor.IsSelecting;
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
                e.CanExecute = editor.Scale < editor.MaxScale;
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
                e.CanExecute = editor.Scale > editor.MinScale;
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
