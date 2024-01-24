using UnityEngine;
using XNode;

public class LogNode : Node, IExecutableNode
{
    [Input] public EnterExitPin enter;
    [Output] public EnterExitPin exit;

    [Input(ShowBackingValue.Never, ConnectionType.Override)]
    public EnterExitPin Message;

    public bool DoesExecutionEnd { get; set; }

    public void Execute()
    {
        var message = GetInputValue<object>("Message");
        Debug.Log(message);
        DoesExecutionEnd = true;
        ExecuteNextNode(nameof(exit));
    }
}