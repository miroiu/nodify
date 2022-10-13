using System.Windows.Input;

namespace Nodify
{
    /// <summary>Gestures used by the <see cref="NodifyEditor"/>.</summary>
    public static class EditorGestures
    {
        /// <summary>The triggers used to start selecting.</summary>
        public static class Selection
        {
            public static InputGesture Replace { get; set; } = new MouseGesture(MouseAction.LeftClick);
            public static InputGesture Remove { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
            public static InputGesture Append { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Shift);
            public static InputGesture Invert { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control);
        }

        public static InputGesture Select { get; } = new SmartInputGesture(SmartInputGesture.Match.Any, Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert);

        /// <summary>The trigger used to start panning.</summary>
        public static InputGesture Pan { get; set; } = new MouseGesture(MouseAction.RightClick);

        /// <summary>The key modifier required to start zooming.</summary>
        public static ModifierKeys Zoom { get; set; } = ModifierKeys.None;

        public static class Connection
        {
            public static InputGesture Split { get; set; } = new MouseGesture(MouseAction.LeftDoubleClick);
            public static InputGesture Disconnect { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
        }

        public static class Connector
        {
            public static InputGesture Disconnect { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
            public static InputGesture Connect { get; set; } = new MouseGesture(MouseAction.LeftClick);
            public static InputGesture Cancel { get; set; } = new MouseGesture(MouseAction.RightClick);
        }
    }

    public class SmartInputGesture : InputGesture
    {
        public enum Match
        {
            Any,
            All
        }

        private readonly InputGesture[] _gestures;
        private readonly Match _operation;

        public SmartInputGesture(Match operation, params InputGesture[] gestures)
        {
            _gestures = gestures;
            _operation = operation;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (_operation == Match.Any)
            {
                return MatchesAny(targetElement, inputEventArgs);
            }

            return MatchesAll(targetElement, inputEventArgs);
        }

        private bool MatchesAll(object targetElement, InputEventArgs inputEventArgs)
        {
            for (int i = 0; i < _gestures.Length; i++)
            {
                if (!_gestures[i].Matches(targetElement, inputEventArgs))
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchesAny(object targetElement, InputEventArgs inputEventArgs)
        {
            for (int i = 0; i < _gestures.Length; i++)
            {
                if (_gestures[i].Matches(targetElement, inputEventArgs))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
