using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// An <see cref="ItemsControl"/> that works with <see cref="DecoratorContainer"/>s.
    /// </summary>
    internal class DecoratorsControl : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(ItemsControl);

        /// <inheritdoc />
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey) 
            => NeedsContainer<DecoratorContainer>(item, out recycleKey);

        /// <inheritdoc />
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey) 
            => new DecoratorContainer();
    }
}
