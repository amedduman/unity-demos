using XNode;

public class StartNode : Node, IStartNode, IExecutableNode
{
    [Output(connectionType: ConnectionType.Override)]
    public EnterExitPin exit;

    public bool DoesExecutionEnd { get; set; }

    public void Execute()
    {
        DoesExecutionEnd = true;
        ExecuteNextNode(nameof(exit));
    }
}