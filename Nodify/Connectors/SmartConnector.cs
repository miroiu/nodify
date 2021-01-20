using System;
using System.Collections.Generic;
using System.Windows;

namespace Nodify
{
    public class SmartConnector : Connector
    {
        public static readonly DependencyProperty IsSmartProperty = DependencyProperty.Register(nameof(IsSmart), typeof(bool), typeof(SmartConnector), new FrameworkPropertyMetadata(BoxValue.True));

        public bool IsSmart
        {
            get => (bool)GetValue(IsSmartProperty);
            set => SetValue(IsSmartProperty, value);
        }

        internal readonly static Dictionary<object, SmartConnector> LoadedConnectors = new Dictionary<object, SmartConnector>();

        private object? _dataContext;

        public Guid Id { get; } = Guid.NewGuid();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (IsSmart)
            {
                _dataContext = DataContext;
                LoadedConnectors[DataContext] = this;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (IsSmart)
            {
                DataContextChanged += OnDataContextChanged;
            }
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

            if (IsSmart)
            {
                LoadedConnectors[DataContext] = this;
                _dataContext = DataContext;
            }
        }

        protected override void OnConnectorUnloaded(object sender, RoutedEventArgs e)
        {
            base.OnConnectorUnloaded(sender, e);

            if (IsSmart && _dataContext != null)
            {
                LoadedConnectors.Remove(_dataContext);
            }
        }
    }
}
