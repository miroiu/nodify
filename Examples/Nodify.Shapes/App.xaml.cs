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
            EditorGestures.Mappings.Connection.Disconnect.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick, KeyModifiers.Alt), new MouseGesture(MouseAction.RightClick));
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                singleView.MainView = new MainView();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
