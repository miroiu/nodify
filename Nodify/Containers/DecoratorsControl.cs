using Nodify.Interactivity;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// An <see cref="ItemsControl"/> that works with <see cref="DecoratorContainer"/>s.
    /// </summary>
    public class DecoratorsControl : ItemsControl, IKeyboardNavigationLayer
    {
        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="DecoratorsControl"/>.
        /// </summary>
        public NodifyEditor? Editor { get; private set; }

        /// <summary>
        /// Gets a list of all <see cref="DecoratorContainer"/>s.
        /// </summary>
        /// <remarks>Cache the result before using it to avoid extra allocations.</remarks>
        protected internal IReadOnlyCollection<DecoratorContainer> DecoratorContainers
        {
            get
            {
                ItemCollection items = Items;
                var containers = new List<DecoratorContainer>(items.Count);

                for (var i = 0; i < items.Count; i++)
                {
                    containers.Add((DecoratorContainer)ItemContainerGenerator.ContainerFromIndex(i));
                }

                return containers;
            }
        }

        static DecoratorsControl()
        {
            FocusableProperty.OverrideMetadata(typeof(DecoratorsControl), new FrameworkPropertyMetadata(BoxValue.False));

            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(DecoratorsControl), new FrameworkPropertyMetadata(KeyboardNavigationMode.None));
            KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(typeof(DecoratorsControl), new FrameworkPropertyMetadata(KeyboardNavigationMode.None));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(DecoratorsControl), new FrameworkPropertyMetadata(KeyboardNavigationMode.None));
        }

        public DecoratorsControl()
        {
            _focusNavigator = new StatefulFocusNavigator<DecoratorContainer>(OnElementFocused);
        }

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is DecoratorContainer;

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
            => new DecoratorContainer(this);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Editor = this.GetParentOfType<NodifyEditor>();

            if (NodifyEditor.AutoRegisterDecoratorsLayer)
            {
                Editor?.RegisterNavigationLayer(this);
            }
        }

        #region Keyboard Navigation

        public KeyboardNavigationLayerId Id { get; } = KeyboardNavigationLayerId.Decorators;
        public IKeyboardFocusTarget<UIElement>? LastFocusedElement => _focusNavigator.LastFocusedElement;

        private readonly StatefulFocusNavigator<DecoratorContainer> _focusNavigator;

        public bool TryMoveFocus(TraversalRequest request)
        {
            return _focusNavigator.TryMoveFocus(request, TryFindContainerToFocus);
        }

        public bool TryRestoreFocus()
        {
            return _focusNavigator.TryRestoreFocus();
        }

        private bool TryFindContainerToFocus(DecoratorContainer? currentElement, TraversalRequest request, out DecoratorContainer? containerToFocus)
        {
            containerToFocus = null;

            if (currentElement is DecoratorContainer focusedContainer)
            {
                containerToFocus = FindNextFocusTarget(focusedContainer, request);
            }
            else if (currentElement is UIElement elem && elem.GetParentOfType<DecoratorContainer>() is DecoratorContainer parentContainer)
            {
                containerToFocus = parentContainer;
            }
            else if (Items.Count > 0 && Editor != null)
            {
                var viewport = new Rect(Editor.ViewportLocation, Editor.ViewportSize);
                var containers = DecoratorContainers;
                containerToFocus = containers.FirstOrDefault(container => viewport.IntersectsWith(((IKeyboardFocusTarget<DecoratorContainer>)container).Bounds))
                    ?? containers.First();
            }

            return containerToFocus != null;
        }

        protected virtual DecoratorContainer? FindNextFocusTarget(DecoratorContainer currentContainer, TraversalRequest request)
        {
            var focusNavigator = new DirectionalFocusNavigator<DecoratorContainer>(DecoratorContainers);
            var result = focusNavigator.FindNextFocusTarget(currentContainer, request);

            return result?.Element;
        }

        protected virtual void OnElementFocused(IKeyboardFocusTarget<DecoratorContainer> target)
        {
            if (NodifyEditor.AutoPanOnNodeFocus)
            {
                Editor?.BringIntoView(target.Bounds, NodifyEditor.BringIntoViewEdgeOffset);
            }
        }

        void IKeyboardNavigationLayer.OnActivated()
        {
            TryRestoreFocus();
        }

        void IKeyboardNavigationLayer.OnDeactivated()
        {
        }

        #endregion
    }
}
