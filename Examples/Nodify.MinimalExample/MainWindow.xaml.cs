using Nodifier;
using Nodifier.Views;
using System.Windows;

namespace Nodify.MinimalExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            NodifyViewManager.RegisterView(typeof(ValueInput<>), typeof(NodeValueInputView));
            NodifyViewManager.RegisterView(typeof(ValueOutput<>), typeof(NodeValueOutputView));
            NodifyViewManager.RegisterView(typeof(ValueEditor<>), typeof(EmptyValueEditorView));
            NodifyViewManager.RegisterView<ValueEditor<string>, StringValueEditorView>();
            NodifyViewManager.RegisterView<ValueEditor<int>, StringValueEditorView>();
            NodifyViewManager.RegisterView<ValueEditor<double>, StringValueEditorView>();
            NodifyViewManager.RegisterView<ValueEditor<bool>, BooleanValueEditorView>();
            NodifyViewManager.RegisterView<IBlueprintConnection, BlueprintConnectionView>();
            NodifyViewManager.RegisterView<IBlueprintPendingConnection, BlueprintPendingConnectionView>();

            InitializeComponent();
        }
    }
}
