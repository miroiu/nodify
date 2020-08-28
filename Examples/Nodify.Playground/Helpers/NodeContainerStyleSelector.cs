using System.Windows;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public class NodeContainerStyleSelector : StyleSelector
    {
        public Style? CommentContainerStyle { get; set; }
        public Style? KnotContainerStyle { get; set; }
        public Style? DefaultContainerStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var result = item switch
            {
                CommentNodeViewModel _ => CommentContainerStyle,
                KnotNodeViewModel _ => KnotContainerStyle,
                _ => DefaultContainerStyle
            };

            return result ?? base.SelectStyle(item, container);
        }
    }
}
