using UnityEngine;
using XNode;

public class DisableNode : Node, IExecutableNode
{
    [Input] public Transform ObjectToDisable;

    public bool DoesExecutionEnd { get; set; }

    public void Execute()
    {
        GetInputValue<Transform>(nameof(ObjectToDisable)).gameObject.SetActive(false);
        DoesExecutionEnd = true;
    }
}