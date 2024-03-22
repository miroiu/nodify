using Avalonia;

namespace Nodify.Compatibility;

internal static class SizeExtensions
{
    public static Vector ToVector(this Size size) => new Vector(size.Width, size.Height);
}