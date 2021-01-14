using System;
using System.Collections.Generic;
using System.Windows;

namespace Nodify
{
    public class SmartConnector : Connector
    {
        internal readonly static Dictionary<object, SmartConnector> LoadedConnectors = new Dictionary<object, SmartConnector>();

        private object? _dataContext;

        public Guid Id { get; } = Guid.NewGuid();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _dataContext = DataContext;
            LoadedConnectors[DataContext] = this;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadedConnectors.Remove(e.OldValue);
            LoadedConnectors[e.NewValue] = this;
            _dataContext = e.NewValue;
        }

        protected override void OnConnectorLoaded(object sender, RoutedEventArgs? e)
        {
            base.OnConnectorLoaded(sender, e);

            LoadedConnectors[DataContext] = this;
            _dataContext = DataContext;
        }

        protected override void OnConnectorUnloaded(object sender, RoutedEventArgs e)
        {
            base.OnConnectorUnloaded(sender, e);

            if (_dataContext != null)
            {
                LoadedConnectors.Remove(_dataContext);
            }
        }
    }
}
