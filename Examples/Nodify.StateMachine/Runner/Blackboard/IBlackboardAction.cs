using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public interface IBlackboardAction
    {
        Task Execute(Blackboard blackboard);
        IReadOnlyList<BlackboardKey> Input { get; }
        IReadOnlyList<BlackboardKey> Output { get; }
    }

    public abstract class BlackboardAction : IBlackboardAction
    {
        public IReadOnlyList<BlackboardKey> Input { get; }
        public IReadOnlyList<BlackboardKey> Output { get; }

        public BlackboardAction(IEnumerable<BlackboardKey>? input = default, IEnumerable<BlackboardKey>? output = default)
        {
            Input = input != null ? new List<BlackboardKey>(input) : new List<BlackboardKey>();
            Output = output != null ? new List<BlackboardKey>(output) : new List<BlackboardKey>();
        }

        public BlackboardAction(BlackboardKey? input = default, BlackboardKey? output = default)
        {
            Input = input.HasValue ? new List<BlackboardKey>() { input.Value } : new List<BlackboardKey>();
            Output = output.HasValue ? new List<BlackboardKey>() { output.Value } : new List<BlackboardKey>();
        }

        public abstract Task Execute(Blackboard blackboard);
    }
}
