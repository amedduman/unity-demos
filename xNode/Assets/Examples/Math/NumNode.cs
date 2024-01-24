using XNode;

public class NumNode : Node
{
    [Output(ShowBackingValue.Always)] public int number;

    public override object GetValue(NodePort port)
    {
        return number;
    }
}