using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public interface IBlackboardCondition
    {
        Task<bool> Evaluate(Blackboard blackboard);
    }
}
