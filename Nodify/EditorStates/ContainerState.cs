namespace Nodify
{
    /// <summary>The base class for container states.</summary>
    public abstract class ContainerState : InputElementState<ContainerState>
    {
        /// <summary>Constructs a new <see cref="ContainerState"/>.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerState(ItemContainer container)
        {
            Container = container;
        }

        /// <summary>The owner of the state.</summary>
        protected ItemContainer Container { get; }

        /// <summary>The owner of the state.</summary>
        protected NodifyEditor Editor => Container.Editor;

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        public virtual void PushState(ContainerState newState) => Container.PushState(newState);

        /// <summary>Pops the current state from the stack.</summary>
        public virtual void PopState() => Container.PopState();
    }
}
