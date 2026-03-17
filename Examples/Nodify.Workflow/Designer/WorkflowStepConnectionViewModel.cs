namespace Nodify.Workflow.Designer;

internal class WorkflowStepConnectionViewModel(WorkflowStepViewModel from, WorkflowStepViewModel to)
{
    public WorkflowStepViewModel From { get; } = from;
    public WorkflowStepViewModel To { get; } = to;
}
