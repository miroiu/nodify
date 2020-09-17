using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public interface IBlackboardAction
    {
        Task Execute(Blackboard blackboard);
    }
}
