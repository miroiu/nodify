using System;
using System.Collections.Generic;
using System.Windows;

namespace Nodify
{
    public abstract class SmartConnection : BaseConnection
    {
        public static readonly DependencyProperty SourceConnectorProperty = DependencyProperty.Register(nameof(SourceConnector), typeof(object), typeof(SmartConnection));
        public static readonly DependencyProperty TargetConnectorProperty = DependencyProperty.Register(nameof(TargetConnector), typeof(object), typeof(SmartConnection));

        private static readonly Dictionary<Guid, int> _connectedConnectors = new Dictionary<Guid, int>();

        public object? SourceConnector
        {
            get => GetValue(SourceConnectorProperty);
            set => SetValue(SourceConnectorProperty, value);
        }

        public object? TargetConnector
        {
            get => GetValue(TargetConnectorProperty);
            set => SetValue(TargetConnectorProperty, value);
        }

        private SmartConnector? _sourceElement;
        public SmartConnector? SourceElement
        {
            get
            {
                if (_sourceElement == null)
                {
                    _sourceElement = GetConnectorElement(SourceConnector);
                }

                return _sourceElement;
            }
        }

        private SmartConnector? _targetElement;
        public SmartConnector? TargetElement
        {
            get
            {
                if (_targetElement == null)
                {
                    _targetElement = GetConnectorElement(TargetConnector);
                }

                return _targetElement;
            }
        }

        private SmartConnector? GetConnectorElement(object? context)
        {
            if (context != null)
            {
                if (SmartConnector.LoadedConnectors.TryGetValue(context, out var result) && result != null)
                {
                    if (!_connectedConnectors.ContainsKey(result.Id))
                    {
                        _connectedConnectors[result.Id] = 0;
                    }

                    ++_connectedConnectors[result.Id];
                    result.IsConnected = true;
                    return result;
                }
            }

            return null;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Unloaded += OnUnloaded;
        }

        protected void OnUnloaded(object sender, RoutedEventArgs e)
        {
            UpdateIsConnected(SourceElement);
            UpdateIsConnected(TargetElement);
        }

        private void UpdateIsConnected(SmartConnector? connector)
        {
            if (connector != null)
            {
                int count = --_connectedConnectors[connector.Id];

                if (count == 0)
                {
                    _connectedConnectors.Remove(connector.Id);
                    connector.IsConnected = false;
                }
            }
        }
    }
}
