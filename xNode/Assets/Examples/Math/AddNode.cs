using UnityEngine;
using XNode;

public class AddNode : Node, IExecutableNode
{
    [Input] public EnterExitPin enter;
    [Output] public EnterExitPin exit;
    [Input] public int a;
    [Input] public int b;
    [Output] public int Result;

    public void Execute()
    {
        a = GetInputValue(nameof(a), a);
        b = GetInputValue(nameof(b), b);
        Result = a + b;
        ExecuteNextNode(nameof(exit));
    }

    public override object GetValue(NodePort port)
    {
        switch (port.fieldName)
        {
            case nameof(Result):
                return Result;
            default:
                return base.GetValue(port);
        }
    }
}