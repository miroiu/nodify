using System.Windows;

namespace Nodify.Events
{
    /// <summary>
    /// Delegate used to notify when an <see cref="ItemContainer"/> is previewing a new location.
    /// </summary>
    /// <param name="newLocation">The new location.</param>
    public delegate void PreviewLocationChanged(Point newLocation);
}
