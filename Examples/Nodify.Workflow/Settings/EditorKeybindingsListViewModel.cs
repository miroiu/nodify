using FluentIcons.Common;
using Nodify.Interactivity;

namespace Nodify.Workflow.Settings
{
    internal class EditorKeybindingsListViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Editor";

        public EditorKeybindingsListViewModel(EditorGestures gestures)
        {
            Title.Value = "Editor";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("Selection")
                {
                    new KeybindingViewModel("Replace", "This gesture will replace the current selection", gestures.Editor.Selection.Replace, Icon.SquareHint),
                    new KeybindingViewModel("Remove", "This gesture will remove items from the current selection", gestures.Editor.Selection.Remove, Icon.SquareEraser),
                    new KeybindingViewModel("Append", "This gesture will append items to the current selection", gestures.Editor.Selection.Append, Icon.SquaresNested),
                    new KeybindingViewModel("Invert", "This gesture will invert the current selection", gestures.Editor.Selection.Invert, Icon.SquareShadow),
                    new KeybindingViewModel("Toggle selected", "This gesture will toggle selection for the focused item", gestures.Editor.Keyboard.ToggleSelected, Icon.Square),
                    new KeybindingViewModel("Deselect all", "This gesture will deselect all the selected items", gestures.Editor.Keyboard.DeselectAll, Icon.SquareDismiss),
                    new KeybindingViewModel("Select all", "This gesture will select all the items in the viewport", gestures.Editor.SelectAll, Icon.SelectObject)
                },
                new KeybindingsGroupViewModel("Navigation")
                {
                    new KeybindingViewModel("Navigate up", "This gesture will move the focus up", gestures.Editor.Keyboard.NavigateSelection.Up, Icon.ArrowUp),
                    new KeybindingViewModel("Navigate down", "This gesture will move the focus down", gestures.Editor.Keyboard.NavigateSelection.Down, Icon.ArrowDown),
                    new KeybindingViewModel("Navigate left", "This gesture will move the focus left", gestures.Editor.Keyboard.NavigateSelection.Left, Icon.ArrowLeft),
                    new KeybindingViewModel("Navigate right", "This gesture will move the focus right", gestures.Editor.Keyboard.NavigateSelection.Right, Icon.ArrowRight),
                    new KeybindingViewModel("Next navigation layer", "This gesture will move focus to the next navigation layer", gestures.Editor.Keyboard.NextNavigationLayer, Icon.ArrowNext),
                    new KeybindingViewModel("Previous navigation layer", "This gesture will move focus to the previous navigation layer", gestures.Editor.Keyboard.PrevNavigationLayer, Icon.ArrowPrevious),
                },
                new KeybindingsGroupViewModel("Panning")
                {
                    new KeybindingViewModel("Pan", "This gesture will move the viewport in the editor", gestures.Editor.Pan, Icon.HandWave),
                    new KeybindingViewModel("Pan up", "This gesture will move the viewport up", gestures.Editor.Keyboard.Pan.Up, Icon.HandWave),
                    new KeybindingViewModel("Pan down", "This gesture will move the viewport down", gestures.Editor.Keyboard.Pan.Down, Icon.HandWave),
                    new KeybindingViewModel("Pan left", "This gesture will move the viewport left", gestures.Editor.Keyboard.Pan.Left, Icon.HandWave),
                    new KeybindingViewModel("Pan right", "This gesture will move the viewport right", gestures.Editor.Keyboard.Pan.Right, Icon.HandWave)
                },
                new KeybindingsGroupViewModel("Dragging")
                {
                    new KeybindingViewModel("Up", "This gesture will move the current selection up", gestures.Editor.Keyboard.DragSelection.Up, Icon.ArrowUp),
                    new KeybindingViewModel("Down", "This gesture will move the current selection down", gestures.Editor.Keyboard.DragSelection.Down, Icon.ArrowDown),
                    new KeybindingViewModel("Left", "This gesture will move the current selection left", gestures.Editor.Keyboard.DragSelection.Left, Icon.ArrowLeft),
                    new KeybindingViewModel("Right", "This gesture will move the current selection right", gestures.Editor.Keyboard.DragSelection.Right, Icon.ArrowRight)
                },
                new KeybindingsGroupViewModel("Others")
                {
                    new KeybindingViewModel("Zoom in", "This gesture will zoom in the viewport", gestures.Editor.ZoomIn, Icon.ZoomIn),
                    new KeybindingViewModel("Zoom out", "This gesture will zoom out the viewport", gestures.Editor.ZoomOut, Icon.ZoomOut),
                    new KeybindingViewModel("Reset viewport", "This gesture will reset the viewport location and scale", gestures.Editor.ResetViewport, Icon.ArrowMoveInward),
                    new KeybindingViewModel("Fit to screen", "This gesture will scale and reposition the viewport to fit as many items as possible", gestures.Editor.FitToScreen, Icon.ZoomFit),
                    new KeybindingViewModel("Cut connections", "This gesture cut the intersecting connections", gestures.Editor.Cutting, Icon.LineDashes),
                    new KeybindingViewModel("Push items", "This gesture will push items in the viewport", gestures.Editor.PushItems, Icon.PaddingRight),
                    new KeybindingViewModel("Cancel action", "This gesture will cancel the current action", gestures.Editor.CancelAction, Icon.DismissCircle),
                }
            ]);
        }
    }

    internal class ItemContainerKeybindingsViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Item container";

        public ItemContainerKeybindingsViewModel(EditorGestures gestures)
        {
            Title.Value = "Item container";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("Selection")
                {
                    new KeybindingViewModel("Replace", "This gesture will replace the current selection with the current item", gestures.ItemContainer.Selection.Replace, Icon.SquareHint),
                    new KeybindingViewModel("Remove", "This gesture will remove the current item from the selection", gestures.ItemContainer.Selection.Remove, Icon.SquareEraser),
                    new KeybindingViewModel("Append", "This gesture will append the current item to the selection", gestures.ItemContainer.Selection.Append, Icon.SquaresNested),
                    new KeybindingViewModel("Invert", "This gesture will invert the current selection of the item", gestures.ItemContainer.Selection.Invert, Icon.SquareShadow),
                },
                new KeybindingsGroupViewModel("Others")
                {
                    new KeybindingViewModel("Start dragging", "This gesture will start moving the current selected item", gestures.ItemContainer.Drag, Icon.ArrowMove),
                    new KeybindingViewModel("Cancel action", "This gesture will cancel the current action", gestures.ItemContainer.CancelAction, Icon.DismissCircle),
                }
            ]);
        }
    }

    internal class ConnectorKeybindingsViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Connector";

        public ConnectorKeybindingsViewModel(EditorGestures gestures)
        {
            Title.Value = "Connector";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("General")
                {
                    new KeybindingViewModel("Start connecting", "This gesture will start connecting the current connector", gestures.Connector.Connect, Icon.PlugConnectedAdd),
                    new KeybindingViewModel("Disconnect", "This gesture will disconnect the current connector", gestures.Connector.Disconnect, Icon.PlugDisconnected),
                    new KeybindingViewModel("Cancel action", "This gesture will cancel the current action", gestures.Connector.CancelAction, Icon.DismissCircle),
                }
            ]);
        }
    }

    internal class ConnectionKeybindingsViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Connection";

        public ConnectionKeybindingsViewModel(EditorGestures gestures)
        {
            Title.Value = "Connection";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("Selection")
                {
                    new KeybindingViewModel("Replace", "This gesture will replace the current selection with the current item", gestures.Connection.Selection.Replace, Icon.SquareHint),
                    new KeybindingViewModel("Remove", "This gesture will remove the current item from the selection", gestures.Connection.Selection.Remove, Icon.SquareEraser),
                    new KeybindingViewModel("Append", "This gesture will append the current item to the selection", gestures.Connection.Selection.Append, Icon.SquaresNested),
                    new KeybindingViewModel("Invert", "This gesture will invert the current selection of the item", gestures.Connection.Selection.Invert, Icon.SquareShadow),
                },
                new KeybindingsGroupViewModel("Others")
                {
                    new KeybindingViewModel("Split", "This gesture will split the current connection", gestures.Connection.Split, Icon.LineHorizontal1DashDotDash),
                    new KeybindingViewModel("Disconnect", "This gesture will remove the current connection", gestures.Connection.Disconnect, Icon.PlugDisconnected),
                }
            ]);
        }
    }

    internal class GroupingNodeKeybindingsViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Grouping node";

        public GroupingNodeKeybindingsViewModel(EditorGestures gestures)
        {
            Title.Value = "Grouping node";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("General")
                {
                    new KeybindingViewModel("Toggle content selection", "This gesture will toggle the content selection", gestures.GroupingNode.ToggleContentSelection, Icon.PlugConnectedAdd),
                }
            ]);
        }
    }

    internal class MinimapKeybindingsViewModel : KeybindingsListViewModel, INavigatable
    {
        public static string RouteKey => $"{KeybindingsSettingsViewModel.RouteKey}/Minimap";

        public MinimapKeybindingsViewModel(EditorGestures gestures)
        {
            Title.Value = "Minimap";

            Keybindings.AddRange(
            [
                new KeybindingsGroupViewModel("General")
                {
                    new KeybindingViewModel("Start connecting", "This gesture will start connecting the current connector", gestures.Connector.Connect, Icon.PlugConnectedAdd),
                    new KeybindingViewModel("Disconnect", "This gesture will disconnect the current connector", gestures.Connector.Disconnect, Icon.PlugDisconnected),
                },
                new KeybindingsGroupViewModel("Panning")
                {
                    new KeybindingViewModel("Pan", "This gesture will move the viewport inside the minimap", gestures.Minimap.DragViewport, Icon.HandWave),
                    new KeybindingViewModel("Pan up", "This gesture will move the viewport up", gestures.Minimap.Pan.Up, Icon.HandWave),
                    new KeybindingViewModel("Pan down", "This gesture will move the viewport down", gestures.Minimap.Pan.Down, Icon.HandWave),
                    new KeybindingViewModel("Pan left", "This gesture will move the viewport left", gestures.Minimap.Pan.Left, Icon.HandWave),
                    new KeybindingViewModel("Pan right", "This gesture will move the viewport right", gestures.Minimap.Pan.Right, Icon.HandWave)
                },
                new KeybindingsGroupViewModel("Others")
                {
                    new KeybindingViewModel("Zoom in", "This gesture will zoom in the viewport", gestures.Minimap.ZoomIn, Icon.ZoomIn),
                    new KeybindingViewModel("Zoom out", "This gesture will zoom out the viewport", gestures.Minimap.ZoomOut, Icon.ZoomOut),
                    new KeybindingViewModel("Reset viewport", "This gesture will reset the viewport location and scale", gestures.Minimap.ResetViewport, Icon.ArrowMoveInward),
                    new KeybindingViewModel("Fit to screen", "This gesture will scale and reposition the viewport to fit as many items as possible", gestures.Editor.FitToScreen, Icon.ZoomFit),
                    new KeybindingViewModel("Cancel action", "This gesture will cancel the current action", gestures.Minimap.CancelAction, Icon.DismissCircle),
                }
            ]);
        }
    }
}
