using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class KnotNode : ContentControl
    {
        static KnotNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KnotNode), new FrameworkPropertyMetadata(typeof(KnotNode)));
        }
    }
}
