using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Nodify
{
    internal class ConnectionsMultiSelector : MultiSelector
    {
        public static readonly DependencyProperty SelectedItemsProperty = NodifyEditor.SelectedItemsProperty.AddOwner(typeof(ConnectionsMultiSelector), new FrameworkPropertyMetadata(default(IList), OnSelectedItemsSourceChanged));
        public static readonly DependencyProperty CanSelectMultipleConnectionsProperty = NodifyEditor.CanSelectMultipleConnectionsProperty.AddOwner(typeof(ConnectionsMultiSelector), new FrameworkPropertyMetadata(BoxValue.True, OnCanSelectMultipleConnectionsChanged, CoerceCanSelectMultipleConnections));

        private static object CoerceCanSelectMultipleConnections(DependencyObject d, object baseValue)
            => ((ConnectionsMultiSelector)d).CanSelectMultipleItems = (bool)baseValue;

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ConnectionsMultiSelector)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        private static void OnCanSelectMultipleConnectionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ConnectionsMultiSelector)d).CanSelectMultipleItems = (bool)e.NewValue;

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
        public bool CanSelectMultipleConnections
        {
            get => (bool)GetValue(CanSelectMultipleConnectionsProperty);
            set => SetValue(CanSelectMultipleConnectionsProperty, value);
        }

        /// <summary>
        /// The <see cref="NodifyEditor"/> that owns this <see cref="ConnectionsMultiSelector"/>.
        /// </summary>
        public NodifyEditor Editor { get; private set; } = default!;

        public ConnectionsMultiSelector()
        {
            CanSelectMultipleItems = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Editor = this.GetParentOfType<NodifyEditor>() ?? throw new NotSupportedException($"{nameof(ConnectionsMultiSelector)} cannot be used outside the {nameof(NodifyEditor)}");
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ConnectionContainer(this);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is ConnectionContainer;

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
    }
}
