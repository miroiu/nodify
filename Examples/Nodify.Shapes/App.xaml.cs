using Nodify.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Shapes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            NodifyEditor.EnableDraggingContainersOptimizations = false;
            NodifyEditor.EnableCuttingLinePreview = true;

            EditorGestures.Mappings.Connection.Disconnect.Value = new AnyGesture(new Interactivity.MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt), new Interactivity.MouseGesture(MouseAction.RightClick));
            EditorGestures.Mappings.Editor.Pan.Value = new AnyGesture(EditorGestures.Mappings.Editor.Pan.Value, new Interactivity.MouseGesture(MouseAction.LeftClick, Key.Space));
        }
    }
}
