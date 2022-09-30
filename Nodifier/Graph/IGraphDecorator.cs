using System.Windows;

namespace Nodifier
{
    public interface IGraphDecorator
    {
        IGraph Graph { get; }
        Point Location { get; set; }
        Size Size { get; }
    }
}
