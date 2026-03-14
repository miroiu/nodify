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
                new KeybindingViewModel("Pan", "This key will move the viewport in the editor", Icon.HandWave, gestures.Editor.Pan)
                {
                    GestureEditor = new MouseGestureSelectorViewModel(gestures.Editor.Pan)
                },
                new KeybindingViewModel("Pan up", "This key will move the viewport up in the editor", Icon.HandWave, gestures.Editor.Keyboard.Pan.Up)
            ]);

        }
    }
}
