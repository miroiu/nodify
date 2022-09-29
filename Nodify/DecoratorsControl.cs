using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// An <see cref="ItemsControl"/> that works with <see cref="DecoratorContainer"/>s.
    /// </summary>
    internal class DecoratorsControl : ItemsControl
    {
        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is DecoratorContainer;

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
            => new DecoratorContainer();
    }
}
