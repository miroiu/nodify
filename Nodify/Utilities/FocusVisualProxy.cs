using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Keeps WPF's focus-visual lifecycle while drawing the indicator in the container's own visual tree.
    /// </summary>
    public sealed class FocusVisualProxy : Control
    {
        private static readonly DependencyProperty OwnerProperty = DependencyProperty.RegisterAttached(
            "Owner", typeof(FocusVisualProxy), typeof(FocusVisualProxy), new PropertyMetadata(defaultValue: null));

        private ItemContainer? _container;
        private FrameworkElement? _visual;

        /// <summary>
        /// Initializes a new focus visual proxy.
        /// </summary>
        public FocusVisualProxy()
        {
            Focusable = false;
            IsTabStop = false;
            IsHitTestVisible = false;
            Opacity = 0;
            Loaded += (sender, args) => UpdateVisibility();
            Unloaded += (sender, args) => Detach();
            IsVisibleChanged += (sender, args) => UpdateVisibility();
        }

        internal static void Refresh(ItemContainer container)
        {
            (container.GetValue(OwnerProperty) as FocusVisualProxy)?.UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            if (!IsVisible)
            {
                Detach();
                return;
            }

            if (_container == null)
            {
                DependencyObject? parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is Adorner))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                _container = (parent as Adorner)?.AdornedElement as ItemContainer;
            }

            if (_container == null)
            {
                return;
            }

            _container.SetValue(OwnerProperty, this);
            var visual = _container.Template?.FindName("PART_FocusVisual", _container) as FrameworkElement;
            if (_visual != visual)
            {
                _visual?.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                _visual = visual;
            }

            if (visual != null)
            {
                visual.SetCurrentValue(StyleProperty, ResolveStyle(_container));
                Opacity = 0;
                visual.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            }
            else
            {
                // Custom templates without the local indicator retain WPF's original overlay behavior.
                Style = ResolveStyle(_container);
                Opacity = 1;
            }
        }

        private static Style? ResolveStyle(ItemContainer container)
        {
            if (container.TryFindResource("ItemContainerFocusVisualStyle") is Style style)
            {
                return style;
            }

            var property = FrameworkElement.FocusVisualStyleProperty;
            var inheritedStyle = (Style?)property.GetMetadata(typeof(ContentControl)).DefaultValue;
            return ReferenceEquals(inheritedStyle, property.DefaultMetadata.DefaultValue)
                ? container.TryFindResource(SystemParameters.FocusVisualStyleKey) as Style
                : inheritedStyle;
        }

        private void Detach()
        {
            if (_container != null && ReferenceEquals(_container.GetValue(OwnerProperty), this))
            {
                _container.ClearValue(OwnerProperty);
                _visual?.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            }

            _container = null;
            _visual = null;
            Opacity = 0;
        }
    }
}
