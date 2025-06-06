﻿using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Nodify
{
    internal sealed class ConnectionsMultiSelector : MultiSelector
    {
        public static readonly DependencyProperty SelectedItemsProperty = NodifyEditor.SelectedItemsProperty.AddOwner(typeof(ConnectionsMultiSelector), new FrameworkPropertyMetadata(default(IList), OnSelectedItemsSourceChanged));
        public static readonly DependencyProperty CanSelectMultipleItemsProperty = NodifyEditor.CanSelectMultipleItemsProperty.AddOwner(typeof(ConnectionsMultiSelector), new FrameworkPropertyMetadata(BoxValue.True, OnCanSelectMultipleItemsChanged, CoerceCanSelectMultipleItems));

        private static void OnCanSelectMultipleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ConnectionsMultiSelector)d).CanSelectMultipleItemsBase = (bool)e.NewValue;

        private static object CoerceCanSelectMultipleItems(DependencyObject d, object baseValue)
            => ((ConnectionsMultiSelector)d).CanSelectMultipleItemsBase = (bool)baseValue;

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ConnectionsMultiSelector)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        /// <summary>
        /// Gets or sets the selected connections in the <see cref="NodifyEditor"/>.
        /// </summary>
        public new IList? SelectedItems
        {
            get => (IList?)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Gets or sets whether multiple connections can be selected.
        /// </summary>
        public new bool CanSelectMultipleItems
        {
            get => (bool)GetValue(CanSelectMultipleItemsProperty);
            set => SetValue(CanSelectMultipleItemsProperty, value);
        }

        private bool CanSelectMultipleItemsBase
        {
            get => base.CanSelectMultipleItems;
            set => base.CanSelectMultipleItems = value;
        }

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="ConnectionsMultiSelector"/>.
        /// </summary>
        public NodifyEditor? Editor { get; private set; }

        protected override DependencyObject GetContainerForItemOverride()
            => new ConnectionContainer(this);

        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is ConnectionContainer;

        public void Select(ConnectionContainer container)
        {
            BeginUpdateSelectedItems();
            var selected = base.SelectedItems;
            selected.Clear();
            selected.Add(container.DataContext);

#if NETCOREAPP3_0_OR_GREATER
            // For some reason the ConnectionContainer.IsSelected property change is not triggered, which prevents the visual update of the child connection.
            // To address this, we manually set the IsSelected property before it is automatically set to true by EndUpdateSelectedItems.
            // Note: This approach will cause bindings to update out of order.
            // It is recommended to handle undo/redo operations using the SelectionChanged event in this case.
            container.IsSelected = true;
#endif

            EndUpdateSelectedItems();

            Editor?.UnselectAll();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Editor = this.GetParentOfType<NodifyEditor>();
        }

        #region Selection Handlers

        private void OnSelectedItemsSourceChanged(IList oldValue, IList newValue)
        {
            if (oldValue is INotifyCollectionChanged oc)
            {
                oc.CollectionChanged -= OnSelectedItemsChanged;
            }

            if (newValue is INotifyCollectionChanged nc)
            {
                nc.CollectionChanged += OnSelectedItemsChanged;
            }

            IList selectedItems = base.SelectedItems;

            BeginUpdateSelectedItems();
            selectedItems.Clear();
            if (newValue != null)
            {
                for (var i = 0; i < newValue.Count; i++)
                {
                    selectedItems.Add(newValue[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        private void OnSelectedItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (!CanSelectMultipleItems)
                return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    base.SelectedItems.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                    IList? newItems = e.NewItems;
                    if (newItems != null)
                    {
                        IList selectedItems = base.SelectedItems;
                        for (var i = 0; i < newItems.Count; i++)
                        {
                            selectedItems.Add(newItems[i]);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    IList? oldItems = e.OldItems;
                    if (oldItems != null)
                    {
                        IList selectedItems = base.SelectedItems;
                        for (var i = 0; i < oldItems.Count; i++)
                        {
                            selectedItems.Remove(oldItems[i]);
                        }
                    }
                    break;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            IList? selected = SelectedItems;
            if (selected != null)
            {
                IList added = e.AddedItems;
                for (var i = 0; i < added.Count; i++)
                {
                    // Ensure no duplicates are added
                    if (!selected.Contains(added[i]))
                    {
                        selected.Add(added[i]);
                    }
                }

                IList removed = e.RemovedItems;
                for (var i = 0; i < removed.Count; i++)
                {
                    selected.Remove(removed[i]);
                }
            }
        }

        #endregion
    }
}
