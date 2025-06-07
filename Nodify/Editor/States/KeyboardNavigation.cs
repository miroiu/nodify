using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        // TODO: Move focus manually 
        public class KeyboardNavigation : InputElementState<NodifyEditor>
        {
            public KeyboardNavigation(NodifyEditor element) : base(element)
            {
                ProcessHandledEvents = true;
            }

            protected override void OnEvent(InputEventArgs e)
            {
                //if (e.RoutedEvent == UIElement.PreviewKeyDownEvent && e is KeyEventArgs args && IsDirectionalNavigationKey(args.Key))
                //{
                //    OnKeyDown(args);
                //    e.Handled = true;
                //}
            }

            private static bool IsDirectionalNavigationKey(Key key)
            {
                return key is Key.Left || key is Key.Right || key is Key.Up || key is Key.Down;
            }

            // TODO: If focus is within, do not allow escaping focus trap unless the escape gesture is performed. (some keys like Space or System Keys could try to escape)
            protected override void OnKeyDown(KeyEventArgs e)
            {
                double cellSize = Element.GridCellSize;
                var gestures = EditorGestures.Mappings.Editor.Keyboard;

                if (Element.IsKeyboardFocusWithin && IsEditorControl(e.OriginalSource))
                {
                    // TODO: Check if the Editor.ActiveFocusScope (ActiveNavigationLayer) is the nodes layer
                    if (gestures.Pan.TryGetNavigationDirection(e, out var panDirection))
                    {
                        var panning = new Vector(-panDirection.X * cellSize, panDirection.Y * cellSize);
                        Element.UpdatePanning(panning);
                        e.Handled = true;
                    }
                    else if (Element.SelectedContainersCount > 0 && gestures.MoveSelection.TryGetNavigationDirection(e, out var dragDirection))
                    {
                        var dragging = new Vector(dragDirection.X * cellSize, -dragDirection.Y * cellSize);
                        Element.BeginDragging();
                        Element.UpdateDragging(dragging);
                        Element.EndDragging();

                        // TODO: Find a way to keep the selection in view

                        e.Handled = true;
                    }
                    else if (gestures.NavigateSelection.TryGetFocusDirection(e, out var direction))
                    {
                        Element.MoveFocus(direction);
                        e.Handled = true;
                    }
                    else if (gestures.NextNavigationLayer.Matches(e.Source, e))
                    {
                        ((INavigationLayerGroup)Element).MoveToNextLayer();
                        e.Handled = true;
                    }
                    else if (gestures.PrevNavigationLayer.Matches(e.Source, e))
                    {
                        ((INavigationLayerGroup)Element).MoveToPrevLayer();
                        e.Handled = true;
                    }
                }
            }

            // TODO: Allow for extensibility because connections can be custom
            private static bool IsEditorControl(object originalSource)
            {
                return originalSource is NodifyEditor || originalSource is ItemContainer || originalSource is Connector || originalSource is BaseConnection;
            }
        }
    }
}
