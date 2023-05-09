namespace Nodifier.Blueprint
{
    public interface INodeSnapshot
    {
        double X { get; set; }
        double Y { get; set; }
        bool IsSelected { get; set; }
    }

    public class BPNodeSnapshot : INodeSnapshot
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsSelected { get; set; }
    }
}
