namespace Nodify.Compatibility;

internal static class ControlCaptureExtensions
{
    internal static void PropagateMouseCapturedWithin(this IInputElement start, bool isCaptured)
    {
        var control = start as Control;
        NodifyEditor? editor = control.FindAncestorOfType<NodifyEditor>(true);
        while (editor != null)
        {
            editor.IsMouseCaptureWithin = isCaptured;
            editor = editor.FindAncestorOfType<NodifyEditor>(false);
        }
    }
}