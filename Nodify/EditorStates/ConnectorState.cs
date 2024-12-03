namespace Nodify
{
    public class ConnectorState : InputElementState<ConnectorState>
    {
        public ConnectorState(Connector connector)
        {
            Connector = connector;
        }

        /// <summary>The owner of the state.</summary>
        protected Connector Connector { get; }

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        public virtual void PushState(ConnectorState newState) => Connector.PushState(newState);

        /// <summary>Pops the current state from the stack.</summary>
        public virtual void PopState() => Connector.PopState();
    }
}
