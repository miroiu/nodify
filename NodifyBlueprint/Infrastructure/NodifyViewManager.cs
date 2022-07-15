using Stylet;
using System;
using System.Collections.Generic;

namespace NodifyBlueprint
{
    public class NodifyViewManager : ViewManager
    {
        private static readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public NodifyViewManager(ViewManagerConfig config) : base(config)
        {
            RegisterView<string, Views.TextView>();
            RegisterView(typeof(ValueInput<>), typeof(Views.NodeValueInputView));
            RegisterView(typeof(ValueOutput<>), typeof(Views.NodeValueOutputView));
            RegisterView(typeof(ValueEditor<>), typeof(Views.EmptyValueEditorView));
            RegisterView<ValueEditor<string>, Views.StringValueEditorView>();
            RegisterView<ValueEditor<int>, Views.StringValueEditorView>();
            RegisterView<ValueEditor<double>, Views.StringValueEditorView>();
            RegisterView<ValueEditor<bool>, Views.BooleanValueEditorView>();
            RegisterView<IGraph, Views.GraphView>();
            RegisterView<IGraphNode, Views.GraphNodeView>();
            RegisterView<IRelayNode, Views.RelayNodeView>();
            RegisterView<ICommentNode, Views.CommentNodeView>();
            RegisterView<IBlueprintConnection, Views.BlueprintConnectionView>();
            RegisterView<IBlueprintPendingConnection, Views.BlueprintPendingConnectionView>();
            RegisterView<IPendingConnection, Views.PendingConnectionView>();
            RegisterView<IConnection, Views.ConnectionView>();
            RegisterView<IRelayConnector, Views.ConnectorView>();
            RegisterView<IConnector, Views.ConnectorView>();
        }

        protected override Type LocateViewForModel(Type modelType)
        {
            if (_mappings.TryGetValue(modelType, out Type? viewType))
            {
                return viewType;
            }

            // Look for unbound generics
            if (modelType.IsGenericType && !modelType.IsGenericTypeDefinition)
            {
                Type genericType = modelType.GetGenericTypeDefinition();
                return LocateViewForModel(genericType);
            }

            foreach (var mapping in _mappings)
            {
                if (mapping.Key.IsAssignableFrom(modelType))
                {
                    return mapping.Value;
                }
            }

            return base.LocateViewForModel(modelType);
        }

        public static void RegisterView<TViewModel, TView>()
        {
            _mappings[typeof(TViewModel)] = typeof(TView);
        }

        public static void RegisterView(Type viewModelType, Type viewType)
        {
            _mappings[viewModelType] = viewType;
        }
    }
}
