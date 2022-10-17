using System.Windows;

namespace Nodifier
{
    public interface IGraphDecorator
    {
        IEditor Editor { get; }
        Point Location { get; set; }
        Size Size { get; }
    }
}
