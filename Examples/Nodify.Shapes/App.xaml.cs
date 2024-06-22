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
            EditorGestures.Mappings.Connection.Disconnect.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt), new MouseGesture(MouseAction.RightClick));
        }
    }
}
